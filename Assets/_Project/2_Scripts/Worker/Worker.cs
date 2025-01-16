using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorkerAnimation))]
public class Worker : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField, Min(0.5f)] private float _moveSpeed;
    
    [Header("Collection")]
    [SerializeField, Range(0.1f, 1.5f), Min(0.1f)] private float _collectStartRadius = 0.2f;
    public ICollectable currentCollectable {get; private set; }
    public float collectTime {get { return _areaData.cropCollectTime; } } 

    [Header("Components")]
    [SerializeField] private SpriteRenderer _hoeRenderer;
    private WorkerAnimation _anim;

    private AreaData _areaData;

    private State _currectState;
    private WorkerCollectingState _collectingState;
    private WorkerWaitingState _waitingState;
    
    public bool isBusy { get; private set; }

    [Zenject.Inject]
    public void Construct(AreaData areaData, WorkerManager workersManager)
    {
        _areaData = areaData;
        workersManager.AddNewWorker(this);

        _collectingState = new WorkerCollectingState(this, _anim, _collectStartRadius);
        _waitingState = new WorkerWaitingState(this, _anim);
        _currectState = _waitingState;
    }

    private void Update() 
    {
        _currectState?.Update();
    } 

    public void StartNewTask(ICollectable collectable)
    {
        currentCollectable = collectable;
        StartWorkProcess();
    }

    private void StartWorkProcess()
    {
        _currectState.Exit();
        _currectState = _collectingState;
        _currectState.Enter();
    }

    public void WaitForJob()
    {
        _currectState.Exit();
        _currectState = _waitingState;
        _currectState.Enter();
    }

    public void MoveToTarget(Transform target)
    {
        Vector3 dif = target.position - transform.position;
        Vector3 moveTo = new Vector3(dif.x, transform.position.y, dif.z);
        transform.position = Vector3.Lerp(transform.position, moveTo, _moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dif);
    }

    private void OnValidate() => _anim ??= GetComponent<WorkerAnimation>();

    private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, _collectStartRadius);
}

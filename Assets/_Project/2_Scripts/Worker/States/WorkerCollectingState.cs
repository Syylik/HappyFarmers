using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class WorkerCollectingState : State
{
    private readonly Worker _handler;
    private readonly Transform _handlerTransform;
    private readonly WorkerAnimation _anim;

    private float _collectStartRadius;

    public WorkerCollectingState(Worker handler, WorkerAnimation workerAnim, float collectStartRadius)
    {
        _handler = handler;
        _anim = workerAnim;
        _handlerTransform = _handler.transform;
        _collectStartRadius = collectStartRadius;
    }

    public override void Enter()
    {
        GoAndCollect(_handler.currentCollectable);
    }

    private async void GoAndCollect(ICollectable targetCollectable)
    {
        _anim.SetWalkingState(true);
        while(targetCollectable != null && Vector3.Distance(_handlerTransform.position, targetCollectable.transform.position) >= _collectStartRadius)
        {
            if(targetCollectable == null) _handler.WaitForJob();
            _handler.MoveToTarget(targetCollectable.transform);
            await Task.Yield();
        }
        _anim.SetCollectingState(true);
        await Task.Delay((int)(_handler.collectTime * 100));
        Collect(targetCollectable);
    }

    private void Collect(ICollectable collectable)
    {
        collectable.Collect();
        _handler.WaitForJob();
    }

    public override void Exit() 
    {
        _anim.SetCollectingState(false);
    }
}

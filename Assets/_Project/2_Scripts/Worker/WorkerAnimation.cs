using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WorkerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private int _walkAnimId = Animator.StringToHash("IsWalking");
    private int _collectAnimId = Animator.StringToHash("Collecting");

    public void SetWalkingState(bool state) => _animator.SetBool(_walkAnimId, state);
    public void SetCollectingState(bool state) 
    {
        SetWalkingState(false);
        _animator.SetBool(_collectAnimId, state);
    }
}

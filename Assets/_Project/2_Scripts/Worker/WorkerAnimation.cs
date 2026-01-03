using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WorkerAnimation : MonoBehaviour
{
    private Animator _anim;
    private int _walkAnimId = Animator.StringToHash("IsWalking");
    private int _collectAnimId = Animator.StringToHash("Collecting");

    public void SetWalkingState(bool state) => _anim.SetBool(_walkAnimId, state);
    public void SetCollectingState(bool state) 
    {
        SetWalkingState(false);
        _anim.SetBool(_collectAnimId, state);
    }

    private void OnValidate() => _anim ??= GetComponent<Animator>();
}

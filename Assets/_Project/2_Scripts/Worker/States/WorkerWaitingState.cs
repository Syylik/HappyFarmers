using UnityEngine;

public class WorkerWaitingState : State
{
    private readonly Worker _handler;
    private readonly WorkerAnimation _anim;

    public WorkerWaitingState(Worker handler, WorkerAnimation anim)
    {
        _handler = handler;
        _anim = anim;
    }

    public override void Enter()
    {
        _anim.SetWalkingState(false);
        // Do something... like a walk around
    }
}

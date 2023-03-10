using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    protected Unit unit;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    protected string animBoolName;

    public UnitState(Unit unit, string animBoolName)
    {
        this.unit = unit;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        DoChecks();
        unit.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        unit.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    public virtual void UseInput(ref bool input) => input = false;
}

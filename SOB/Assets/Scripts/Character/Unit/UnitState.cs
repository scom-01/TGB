using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState
{
    protected Unit unit;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    protected string animBoolName;
    protected Movement Movement
    {
        get => movement ?? unit.Core.GetCoreComponent(ref movement);
    }
    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? unit.Core.GetCoreComponent(ref collisionSenses);
    }
    protected UnitStats UnitStats
    {
        get => unitStats ?? unit.Core.GetCoreComponent(ref unitStats);
    }

    protected EffectManager EffectManager
    {
        get => effectManager ?? unit.Core.GetCoreComponent(ref effectManager);
    }
    protected SoundEffect SoundEffect
    {
        get => soundEffect ?? unit.Core.GetCoreComponent(ref soundEffect);
    }
    protected Death Death
    {
        get => death ?? unit.Core.GetCoreComponent(ref death);
    }


    private Movement movement;
    private CollisionSenses collisionSenses;
    private UnitStats unitStats;
    private EffectManager effectManager;
    private SoundEffect soundEffect;
    private Death death;
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
        Debug.Log(unit.name + " Enter State : " + animBoolName);
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

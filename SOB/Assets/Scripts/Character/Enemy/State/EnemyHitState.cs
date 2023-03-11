using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHitState : EnemyState
{
    public EnemyHitState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.speed = 0.0f;
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= unit.UnitData.knockBackDuration + startTime)
        {
            unit.Anim.speed = 1.0f;
            unit.Core.GetCoreComponent<DamageReceiver>().isHit = false;
            IdleState();            
        }
    }
    public abstract void IdleState();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

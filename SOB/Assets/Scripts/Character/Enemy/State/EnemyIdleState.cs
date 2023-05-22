using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyIdleState : EnemyState
{
    protected bool isIdleTimeOver;

    protected float idleTime;

    public EnemyIdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(EnemyCollisionSenses.isUnitInDetectedArea)
        {
            enemy.SetEOE(EnemyCollisionSenses.UnitDetectArea?.GetComponent<Unit>());
            RunState();            
        }

        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public abstract void RunState();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(enemy.enemyData.minIdleTime, enemy.enemyData.maxIdleTime);
    }
}

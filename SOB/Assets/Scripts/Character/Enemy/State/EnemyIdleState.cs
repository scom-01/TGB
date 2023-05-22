using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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
        }

        if (enemy.EOE != null)
        {
            if (((enemy.transform.position.x - enemy.EOE.transform.position.x) > 0) != Movement.FancingDirection > 0)
            {
                Movement.Flip();
            }
            RunState();
        }

        if (Time.time >= startTime + idleTime)
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

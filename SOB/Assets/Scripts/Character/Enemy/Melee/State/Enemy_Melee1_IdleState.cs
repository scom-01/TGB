using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1_IdleState : EnemyIdleState
{
    private Enemy_Melee1 enemy_Melee1;
    public Enemy_Melee1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
        Debug.Log("Enemy_Melee1_Idle");
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (CollisionSenses.UnitDectected)
        {
            RunState();
        }

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }
    public override void RunState()
    {
        base.RunState();
        unit.FSM.ChangeState(enemy_Melee1.RunState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

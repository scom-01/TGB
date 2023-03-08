using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1_AttackState : EnemyAttackState
{
    private Enemy_Melee1 enemy_Melee1;

    public Enemy_Melee1_AttackState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
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

    public override void IdleState()
    {
        base.IdleState();
        enemy.FSM.ChangeState(enemy_Melee1.IdleState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            IdleState();
        }
    }
}

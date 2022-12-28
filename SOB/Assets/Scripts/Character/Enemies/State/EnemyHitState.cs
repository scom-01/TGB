using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(Enemy enemy, EnemyFSM fSM, EnemyData enemyData, string animBoolName) : base(enemy, fSM, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.enemyCore.Movement.SetVelocityX(enemyData.knockBackSpeed * enemy.damageDirection);
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= enemyData.knockBackDuration + startTime)
        {
            FSM.ChangeState(enemy.RunState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

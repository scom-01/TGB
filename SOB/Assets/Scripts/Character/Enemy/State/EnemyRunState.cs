using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    public EnemyRunState(Enemy enemy, EnemyFSM fSM, EnemyData enemyData, string animBoolName) : base(enemy, fSM, enemyData, animBoolName)
    {
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

        if(!enemy.enemyCore.CollisionSenses.CheckIfCliff || enemy.enemyCore.CollisionSenses.CheckIfTouchingWall || enemy.enemyCore.CollisionSenses.CheckIfTouching)
        {
            enemy.enemyCore.Movement.Flip();
        }
        else
        {
            enemy.enemyCore.Movement.SetVelocityX(enemyData.movementVelocity * enemy.enemyCore.Movement.FancingDirection);
        }
        //CheckIfCliff();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

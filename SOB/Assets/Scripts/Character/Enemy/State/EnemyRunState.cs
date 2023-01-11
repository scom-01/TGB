using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    public EnemyRunState(Unit unit, string animBoolName) : base(unit, animBoolName)
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

        if(enemy.core.enemyMovement.knockback)
        {
            Debug.Log(enemy.name + " ³Ë¹é»óÅÂ");
            return;
        }

        if(!enemy.core.enemyCollisionSenses.CheckIfCliff || enemy.Core.CollisionSenses.CheckIfTouchingWall || enemy.core.enemyCollisionSenses.CheckIfTouching)
        {
            enemy.Core.Movement.Flip();
        }
        else
        {
            enemy.core.enemyMovement.SetVelocityX(enemy.enemyData.commonStats.movementVelocity * enemy.core.enemyMovement.FancingDirection);
        }
        //CheckIfCliff();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    private bool checkifCliff;
    private bool checkifTouching;
    private bool checkifTouchingWall;
    public EnemyRunState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        checkifCliff = CollisionSenses.CheckIfCliff;
        checkifTouchingWall = CollisionSenses.CheckIfTouchingWall;
        checkifTouching = CollisionSenses.CheckIfTouching;
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

        //if(Movement.knockback)
        //{
        //    Debug.Log(enemy.name + " 넉백상태");
        //    return;
        //}

        if (!checkifCliff || checkifTouching || checkifTouchingWall )
        {
            Movement.Flip();
        }
        else
        {
            Movement.SetVelocityX(enemy.enemyData.statsStats.MovementVelocity * Movement.FancingDirection);
        }
        //CheckIfCliff();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

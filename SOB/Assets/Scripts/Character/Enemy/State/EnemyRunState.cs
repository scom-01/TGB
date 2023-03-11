using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyRunState : EnemyState
{
    private bool checkifCliff;
    private bool checkifTouchingGrounded;
    private bool checkifTouchingWall;
    public EnemyRunState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        checkifCliff = CollisionSenses.CheckIfCliff;
        checkifTouchingWall = CollisionSenses.CheckIfTouchingWall;
        checkifTouchingGrounded = CollisionSenses.CheckIfStayGrounded;
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


        if (CollisionSenses.UnitDectected && unit.Inventory.weapons.Count > 0)
        {
            Enemy_Attack();
        }

        if (!checkifCliff || checkifTouchingGrounded || checkifTouchingWall)
        {
            Movement.Flip();
        }
        else
        {
            Movement.SetVelocityX(enemy.enemyData.statsStats.MovementVelocity * Movement.FancingDirection);
        }
    }

    public abstract void Enemy_Attack();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

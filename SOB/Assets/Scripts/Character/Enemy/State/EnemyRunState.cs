using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
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

        //if(Movement.knockback)
        //{
        //    Debug.Log(enemy.name + " 넉백상태");
        //    return;
        //}

        if (!checkifCliff || checkifTouchingGrounded || checkifTouchingWall)
        {
            Movement.Flip();
        }
        else
        {
            Movement.SetVelocityX(enemy.enemyData.statsStats.MovementVelocity * Movement.FancingDirection);
        }
    }

    public virtual void Enemy_Attack()
    {
        Debug.Log("Enemy_Attack");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

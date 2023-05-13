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

        checkifCliff = EnemyCollisionSenses.CheckIfCliff;
        checkifTouchingWall = EnemyCollisionSenses.CheckIfTouchingWall;
        checkifTouchingGrounded = EnemyCollisionSenses.CheckIfStayGrounded;
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

        if (unit.UnitData.GetType() != typeof(EnemyData))
            return;

        if (EnemyCollisionSenses.UnitInAttackArea && unit.Inventory.weapons.Count > 0)
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

using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyRunState : EnemyState
{
    private bool checkifCliff;
    private bool checkifCliffBack;
    private bool checkifTouchingGrounded;
    private bool checkifTouchingWall;
    private bool checkifTouchingWallBack;

    public EnemyRunState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        checkifCliff = EnemyCollisionSenses.CheckIfCliff;
        checkifCliffBack = EnemyCollisionSenses.CheckIfCliffBack;
        checkifTouchingWall = EnemyCollisionSenses.CheckIfTouchingWall;
        checkifTouchingWallBack = EnemyCollisionSenses.CheckIfTouchingWallBack;
        checkifTouchingGrounded = EnemyCollisionSenses.CheckIfStayGrounded;
                
        if(!isGrounded)
        {
            IdleState();
            return;
        }

        if((!checkifCliff && !checkifCliffBack ) || (checkifTouchingWall && checkifTouchingWallBack))
        {
            enemy.SetTarget(null);
            IdleState();
            return;
        }
        else if(!checkifCliff || checkifTouchingWall)
        {
            Movement.SetVelocityX(0);
            Movement.Flip();
        }
        Movement.SetVelocityX(enemy.enemyData.statsStats.MovementVelocity * Movement.FancingDirection);
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

        if (EnemyCollisionSenses.isUnitInAttackArea && unit.Inventory.Weapon != null)
        {
            Debug.Log($"{enemy.name}'s EOE = {enemy.TargetUnit}");
            Enemy_Attack();
            return;
        }

        //일직선 상
        if (EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
        {
            FlipToTarget();
        }

        //패턴 딜레이
        if (Time.time >= startTime + enemy.enemyData.maxIdleTime)
        {
            isDelayCheck = true;
        }

        if (!isDelayCheck)
            return;
                
        isDelayCheck = false;
        IdleState();
    }

    public abstract void Enemy_Attack();
    public abstract void IdleState();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

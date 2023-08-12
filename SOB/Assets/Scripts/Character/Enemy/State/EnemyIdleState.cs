using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine;

public abstract class EnemyIdleState : EnemyState
{
    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool checkifCliff;
    protected bool isDelayCheck = false;

    private bool checkifCliffBack;
    private bool checkifTouchingGrounded;
    private bool checkifTouchingWall;
    private bool checkifTouchingWallBack;


    public EnemyIdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        checkifCliff = EnemyCollisionSenses.CheckIfCliff;
        checkifCliffBack = EnemyCollisionSenses.CheckIfCliffBack;
        checkifTouchingWall = EnemyCollisionSenses.CheckIfTouchingWall;
        checkifTouchingWallBack = EnemyCollisionSenses.CheckIfTouchingWallBack;
        checkifTouchingGrounded = EnemyCollisionSenses.CheckIfStayGrounded;

        if(EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
        {
            enemy.SetTarget(EnemyCollisionSenses.UnitFrontDetectArea?.GetComponent<Unit>());
        }
        

        if (enemy.TargetUnit != null)
        {
            if ((!checkifCliff && !checkifCliffBack) || (checkifTouchingWall && checkifTouchingWallBack))
            {
                FlipToTarget();
            }

            //패턴 딜레이
            if (Time.time >= startTime + enemy.enemyData.minIdleTime)
            {
                isDelayCheck = true;
            }
            Pattern();
            return;
        }

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public abstract void Pattern();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(enemy.enemyData.minIdleTime, enemy.enemyData.maxIdleTime);
    }
}

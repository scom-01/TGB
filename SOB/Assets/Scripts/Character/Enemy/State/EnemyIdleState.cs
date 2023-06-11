using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public abstract class EnemyIdleState : EnemyState
{
    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool checkifCliff;
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

        //if (enemy.enemyData.enemy_form == ENEMY_Form.Grounded && !isGrounded)
        //{            
        //    if(checkifTouchingGrounded)
        //    {
        //        enemy.transform.Translate(Vector2.up * 0.05f);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        if(EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
        {
            enemy.SetTarget(EnemyCollisionSenses.UnitFrontDetectArea?.GetComponent<Unit>());
        }

        if (enemy.TargetUnit != null)
        {
            if ((!checkifCliff && !checkifCliffBack) || (checkifTouchingWall && checkifTouchingWallBack))
            {
                FollowTarget();
            }
            ChangeState();
            return;
        }

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public abstract void ChangeState();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(enemy.enemyData.minIdleTime, enemy.enemyData.maxIdleTime);
    }
}

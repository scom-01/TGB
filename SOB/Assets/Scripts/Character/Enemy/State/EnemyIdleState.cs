using SOB.CoreSystem;
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

        checkifCliff = EnemyCollisionSenses.CheckIfCliff;
        checkifCliffBack = EnemyCollisionSenses.CheckIfCliffBack;
        checkifTouchingWall = EnemyCollisionSenses.CheckIfTouchingWall;
        checkifTouchingWallBack = EnemyCollisionSenses.CheckIfTouchingWallBack;
        checkifTouchingGrounded = EnemyCollisionSenses.CheckIfStayGrounded;
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

        if(EnemyCollisionSenses.isUnitDetectedCircle)// EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
        {
            enemy.SetTarget(EnemyCollisionSenses.UnitDetectedCircle?.GetComponent<Unit>());
        }
        
    
        //패턴 딜레이
        if (Time.time >= startTime + Random.Range(enemy.enemyData.minIdleTime, enemy.enemyData.maxIdleTime))
        {
            isDelayCheck = true;
        }
            
        //타겟 방향 회전
        FlipToTarget();


        if (!isDelayCheck)
            return;

        isDelayCheck = false;

        if (enemy.TargetUnit == null)
        {
            MoveState();
            return;
        }

        Pattern();
        return;
    }

    public abstract void MoveState();

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(enemy.enemyData.minIdleTime, enemy.enemyData.maxIdleTime);
    }
}

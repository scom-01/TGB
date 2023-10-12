using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;                 //좌우 입력값
    private bool JumpInput;             //점프 입력값
    private bool JumpInputStop;         //점프 입력 해체 값
    private bool dashInput;             //Dash 키 입력값
    private bool skill1Input;           //skill1키 입력값
    private bool skill2Input;           //skill2키 입력값

    //Checks
    private bool isGrounded;            //바닥 체크
    private bool isTouchingWall;        //전방 벽과 붙어있는지 체크
    private bool isTouchingWallBack;    //후방 벽과 붙어있는지 체크
    private bool oldIsTouchingWall;     //Old 전방 벽과 붙어있는지 체크
    private bool oldIsTouchingWallBack; //Old 후방 벽과 붙어있는지 체크
    private bool isTouchingLedge;       //LedgeCheck오브젝트가 벽을 체크하는지

    private bool coyoteTime;            //코요테 타임 체크
    private bool wallJumpCoyoteTime;    //벽 점프 코요테 타임 체크
    private bool isJumping;             //점프 중인지 체크

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = CollisionSenses.CheckIfGrounded || CollisionSenses.CheckIfPlatform;
        isTouchingWall = CollisionSenses.CheckIfTouchingWall;
        isTouchingWallBack = CollisionSenses.CheckIfTouchingWallBack;

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        player.Anim.SetFloat("yVelocity", 0);
        player.Anim.SetFloat("xVelocity", 0);

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        JumpInputStop = player.InputHandler.JumpInputStop;

        CheckJumpMultiplier();

        dashInput = player.InputHandler.DashInput;
        skill1Input = player.InputHandler.Skill1Input;
        skill2Input = player.InputHandler.Skill2Input;

        if (player.InputHandler.ActionInputs[(int)CombatInputs.primary])
        {
            player.PrimaryAttackState.SetWeapon(player.Inventory.Weapon);
            if (player.PrimaryAttackState.CheckCommand(isGrounded, ref player.Inventory.Weapon.CommandList))
            {
                player.FSM.ChangeState(player.PrimaryAttackState);
            }
        }
        else if (player.InputHandler.ActionInputs[(int)CombatInputs.secondary])
        {
            player.SecondaryAttackState.SetWeapon(player.Inventory.Weapon);
            if (player.SecondaryAttackState.CheckCommand(isGrounded, ref player.Inventory.Weapon.CommandList))
            {
                player.FSM.ChangeState(player.SecondaryAttackState);
            }
        }

        //Ground 착지
        else if (isGrounded && Movement.CurrentVelocity.y <= 0.01f)
        {
            player.FSM.ChangeState(player.LandState);
            return;
        }
        else if (JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            unit.isFixedMovement = false;
            isTouchingWall = CollisionSenses.CheckIfTouchingWall;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            player.FSM.ChangeState(player.WallJumpState);
            return;
        }
        else if (JumpInput && player.JumpState.CanJump() && !player.CC2D.isTrigger)
        {
            coyoteTime = false;
            player.FSM.ChangeState(player.JumpState);
            return;
        }
        else if (isTouchingWall && xInput == Movement.FancingDirection && Movement.CurrentVelocity.y <= 0f)
        {
            player.FSM.ChangeState(player.WallSlideState);
            return;
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            player.Anim.SetBool("JumpFlip", false);
            player.FSM.ChangeState(player.DashState);
            return;
        }

        Movement.CheckIfShouldFlip(xInput);
        //if(Movement.CanSetVelocity)
        if (!unit.isFixedMovement)
            Movement.SetVelocityX(UnitStats.DefaultMoveSpeed * ((100f + UnitStats.MoveSpeed) / 100f) * xInput);

        player.Anim.SetFloat("yVelocity", Mathf.Clamp(Movement.CurrentVelocity.y, -3, 13));
        player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
    }


    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (JumpInputStop)
            {
                //TODO:InputTime으로 점프 Velocity 조절 
                //Movement.SetVelocityY(Movement.CurrentVelocity.y * player.playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + player.playerData.coyeteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + player.playerData.coyeteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
    public void SetIsJumping() => isJumping = true;
}
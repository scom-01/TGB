using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;                 //좌우 입력값
    private bool JumpInput;             //점프 입력값
    private bool JumpInputStop;         //점프 입력 해체 값
    private bool grabInput;             //그랩 입력값
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
    public PlayerInAirState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingLedge();

        /*if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }*/

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

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        JumpInputStop = player.InputHandler.JumpInputStop;
        CheckJumpMultiplier();

        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        skill1Input = player.InputHandler.Skill1Input;
        skill2Input = player.InputHandler.Skill2Input;

        if (skill1Input && !isGrounded)
        {
            FSM.ChangeState(player.AirAttackState);
        }
        else if(isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            FSM.ChangeState(player.LandState);
        }
        /*else if(isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            FSM.ChangeState(player.LedgeClimbState);
        }*/
        else if(JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            FSM.ChangeState(player.WallJumpState);
        }
        else if(JumpInput && player.JumpState.CanJump())
        {
            coyoteTime = false; 
            FSM.ChangeState(player.JumpState);
        }
        /*else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            FSM.ChangeState(player.WallGrabState);
        }*/
        else if(isTouchingWall && xInput == player.FancingDirection && player.CurrentVelocity.y <= 0f)
        {
            FSM.ChangeState(player.WallSlideState);
        }
        
        else if(dashInput && player.DashState.CheckIfCanDash())
        {
            if(player.DashState.DashCount > 0)
            {
                FSM.ChangeState(player.DashState);
            }
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", Mathf.Clamp(player.CurrentVelocity.y, -3, 13));
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (JumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyeteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void  CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyeteTime)
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

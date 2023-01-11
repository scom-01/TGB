using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInputHandler;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;                 //�¿� �Է°�
    private bool JumpInput;             //���� �Է°�
    private bool JumpInputStop;         //���� �Է� ��ü ��
    private bool grabInput;             //�׷� �Է°�
    private bool dashInput;             //Dash Ű �Է°�
    private bool skill1Input;           //skill1Ű �Է°�
    private bool skill2Input;           //skill2Ű �Է°�

    //Checks
    private bool isGrounded;            //�ٴ� üũ
    private bool isTouchingWall;        //���� ���� �پ��ִ��� üũ
    private bool isTouchingWallBack;    //�Ĺ� ���� �پ��ִ��� üũ
    private bool oldIsTouchingWall;     //Old ���� ���� �پ��ִ��� üũ
    private bool oldIsTouchingWallBack; //Old �Ĺ� ���� �پ��ִ��� üũ
    private bool isTouchingLedge;       //LedgeCheck������Ʈ�� ���� üũ�ϴ���

    private bool coyoteTime;            //�ڿ��� Ÿ�� üũ
    private bool wallJumpCoyoteTime;    //�� ���� �ڿ��� Ÿ�� üũ
    private bool isJumping;             //���� ������ üũ

    private float startWallJumpCoyoteTime;
    public PlayerInAirState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.Core.CollisionSenses.CheckIfGrounded;
        isTouchingWall = player.Core.CollisionSenses.CheckIfTouchingWall;
        isTouchingWallBack = player.Core.CollisionSenses.CheckIfTouchingWallBack;
        isTouchingLedge = player.Core.CollisionSenses.CheckIfTouchingLedge;

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

        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            player.PrimaryAttackState.SetWeapon(player.Inventory.weapons[(int)CombatInputs.primary]);
            FSM.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            player.SecondaryAttackState.SetWeapon(player.Inventory.weapons[(int)CombatInputs.secondary]);
            FSM.ChangeState(player.SecondaryAttackState);            
        }
        else if(isGrounded && player.Core.Movement.CurrentVelocity.y < 0.01f)
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
            isTouchingWall = player.Core.CollisionSenses.CheckIfTouchingWall;
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
        else if(isTouchingWall && xInput == player.Core.Movement.FancingDirection && player.Core.Movement.CurrentVelocity.y <= 0f)
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
            player.Core.Movement.CheckIfShouldFlip(xInput);
            player.Core.Movement.SetVelocityX(playerData.commonstats.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", Mathf.Clamp(player.Core.Movement.CurrentVelocity.y, -3, 13));
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.Core.Movement.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (JumpInputStop)
            {
                player.Core.Movement.SetVelocityY(player.Core.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.Core.Movement.CurrentVelocity.y <= 0f)
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
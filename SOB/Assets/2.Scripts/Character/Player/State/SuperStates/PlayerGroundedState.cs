using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGroundedState : PlayerState
{
    //Input
    protected int xInput;           //x�� �̵� �Է°�
    private bool JumpInput;         //���� �Է°�
    private bool grabInput;         //grab �Է°�
    private bool dashInput;         //Dash �Է°�
    private bool blockInput;        //Block �Է°�
    private bool skill1Input;       //Skill1 �Է°�
    //Checks
    private bool isGrounded;        //Grounded üũ
    private bool isTouchingWall;    //�� üũ 
    private bool isTouchingLedge;   //Ledgeüũ

    public PlayerGroundedState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge(); 
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
        player.BlockState.ResetCanBlock();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        blockInput = player.InputHandler.BlockInput;
        skill1Input = player.InputHandler.Skill1Input;

        if (JumpInput && player.JumpState.CanJump())
        {
            FSM.ChangeState(player.JumpState);
        }
        else if(!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            FSM.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            FSM.ChangeState(player.DashState);
        }
        else if(skill1Input)
        {
            FSM.ChangeState(player.AttackState);
        }
        else if(blockInput && player.BlockState.CheckIfCanBlock())
        {
            FSM.ChangeState(player.BlockState);
        }
        /*else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            FSM.ChangeState(player.WallGrabState);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

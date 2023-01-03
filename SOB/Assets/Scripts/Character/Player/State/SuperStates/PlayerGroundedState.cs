using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlayerInputHandler;

public class PlayerGroundedState : PlayerState
{
    //Input
    protected int xInput;           //x축 이동 입력값
    private bool JumpInput;         //점프 입력값
    private bool grabInput;         //grab 입력값
    private bool dashInput;         //Dash 입력값
    private bool blockInput;        //Block 입력값
    private bool skill1Input;       //Skill1 입력값
    private bool skill2Input;       //Skill2 입력값
    //Checks
    private bool isGrounded;        //Grounded 체크
    private bool isTouchingWall;    //벽 체크 
    private bool isTouchingLedge;   //Ledge체크

    public PlayerGroundedState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.Core.CollisionSenses.CheckIfGrounded;
        isTouchingWall = player.Core.CollisionSenses.CheckIfTouchingWall;
        isTouchingLedge = player.Core.CollisionSenses.CheckIfTouchingLedge; 
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
        //player.BlockState.ResetCanBlock();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player.DashState.CheckIfResetDash())
        {
            player.DashState.ResetDash(playerData.dashCount);
        }

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        blockInput = player.InputHandler.BlockInput;
        
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            FSM.ChangeState(player.PrimaryAttackState);
        }
        else if(player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            FSM.ChangeState(player.SecondaryAttackState);
        }
        else if (JumpInput && player.JumpState.CanJump())
        {
            FSM.ChangeState(player.JumpState);        
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            FSM.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            FSM.ChangeState(player.DashState);
        }
        else if(blockInput)
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

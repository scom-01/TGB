using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;

    protected int xInput;
    protected int yInput;
    public PlayerTouchingWallState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.Core.CollisionSenses.CheckIfGrounded;
        isTouchingWall = player.Core.CollisionSenses.CheckIfTouchingWall;
        isTouchingLedge = player.Core.CollisionSenses.CheckIfTouchingLedge;

        /*if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }*/
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

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;

        if(jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            FSM.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            FSM.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (xInput != player.Core.Movement.FancingDirection && !grabInput))
        {
            FSM.ChangeState(player.InAirState);
        }    
        /*else if(isTouchingWall && !isTouchingLedge)
        {
            FSM.ChangeState(player.LedgeClimbState);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool jumpInput;

    protected int xInput;
    protected int yInput;

    public PlayerTouchingWallState(Unit unit, string animBoolName) : base(unit, animBoolName)
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

        isGrounded = CollisionSenses.CheckIfGrounded;
        isTouchingWall = CollisionSenses.CheckIfTouchingWall;
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
        jumpInput = player.InputHandler.JumpInput;

        if(jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            player.FSM.ChangeState(player.WallJumpState);
        }
        else if (isGrounded || CollisionSenses.CheckIfPlatform)
        {
            player.FSM.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (xInput != Movement.FancingDirection))
        {
            player.FSM.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

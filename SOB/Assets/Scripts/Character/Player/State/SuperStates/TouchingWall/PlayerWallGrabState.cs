using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    
    public PlayerWallGrabState(Unit unit, string animBoolName) : base(unit, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;

        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;

        HoldPosition();
        if (yInput > 0)
        {
            //FSM.ChangeState(player.WallClimbState);
        }
        else if (yInput < 0 || !grabInput)
        {
            player.FSM.ChangeState(player.WallSlideState);
        }
    }
    private void HoldPosition()
    {
        player.transform.position = holdPosition;
        Movement.SetVelocityX(0f);
        Movement.SetVelocityY(0f);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    
    public PlayerWallJumpState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.Core.Movement.SetVelocity(player.playerData.wallJumpVelocity, player.playerData.wallJumpAngle, wallJumpDirection);
        Debug.Log("Wall Jump Velocity = " + player.Core.Movement.CurrentVelocity);
        player.Core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", Mathf.Clamp(player.Core.Movement.CurrentVelocity.y, -3, 13));
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.Core.Movement.CurrentVelocity.x));

        if(Time.time >= startTime + player.playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if(isTouchingWall)
        {
            wallJumpDirection = -player.Core.Movement.FancingDirection;
        }
        else
        {
            wallJumpDirection = player.Core.Movement.FancingDirection;
        }
    }
}

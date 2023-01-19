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
        player.InputHandler.UseInput(ref player.InputHandler.JumpInput);
        player.JumpState.ResetAmountOfJumpsLeft();
        Movement.SetVelocity(player.playerData.wallJumpVelocity, player.playerData.wallJumpAngle, wallJumpDirection);
        Debug.Log("Wall Jump Velocity = " + Movement.CurrentVelocity);
        Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", Mathf.Clamp(Movement.CurrentVelocity.y, -3, 13));
        player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

        if(Time.time >= startTime + player.playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if(isTouchingWall)
        {
            wallJumpDirection = -Movement.FancingDirection;
        }
        else
        {
            wallJumpDirection = Movement.FancingDirection;
        }
    }
}

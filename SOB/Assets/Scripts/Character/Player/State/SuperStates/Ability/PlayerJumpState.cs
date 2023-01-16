using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft;

    public PlayerJumpState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        amountOfJumpLeft = player.playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        Movement.SetVelocityY(player.playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if(amountOfJumpLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpLeft = player.playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpLeft--;
}

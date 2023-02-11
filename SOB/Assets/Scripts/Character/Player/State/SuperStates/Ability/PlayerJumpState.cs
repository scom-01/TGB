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
        isAbilityDone = true;
        Jump();
    }

    public void Jump()
    {
        player.InputHandler.UseInput(ref player.InputHandler.JumpInput);
        Movement.SetVelocityY(UnitStats.StatsData.JumpVelocity);
        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump() => (amountOfJumpLeft > 0);

    public void ResetAmountOfJumpsLeft() => amountOfJumpLeft = player.playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpLeft--;
}

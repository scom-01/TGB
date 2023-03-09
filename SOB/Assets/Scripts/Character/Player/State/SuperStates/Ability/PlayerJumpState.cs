using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft;
    private GameObject Jump_Effect;

    public PlayerJumpState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        amountOfJumpLeft = player.playerData.amountOfJumps;

        if(Jump_Effect == null)
        {
            Jump_Effect = Resources.Load<GameObject>("Prefabs/Particle/Jump_Smoke");
        }        
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
        if(amountOfJumpLeft < player.playerData.amountOfJumps)
        {
            player.Core.GetCoreComponent<ParticleManager>().StartParticles(Jump_Effect, CollisionSenses.GroundCheck.position);
        }
        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump() => (amountOfJumpLeft > 0);

    public void ResetAmountOfJumpsLeft() => amountOfJumpLeft = player.playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpLeft--;
}

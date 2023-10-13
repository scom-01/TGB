using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft;
    private GameObject Jump_Effect;
    private AudioClip Jump_Sfx;

    public PlayerJumpState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        amountOfJumpLeft = player.playerData.amountOfJumps;

        if (Jump_Effect == null)
        {
            Jump_Effect = Resources.Load<GameObject>("Prefabs/Effects/Jump_Smoke");
        }
        if (Jump_Sfx == null)
        {
            Jump_Sfx = Resources.Load<AudioClip>("Sounds/Effects/SFX_Jump_01");
        }
    }

    public override void Enter()
    {
        base.Enter();
        unit.RB.gravityScale = unit.UnitData.UnitGravity;
        isAbilityDone = true;
        Jump();        
    }

    private void Jump()
    {
        Debug.Log("Jump");
        player.isFixedMovement = false;
        player.InputHandler.UseInput(ref player.InputHandler.JumpInput);
        Movement.SetVelocityY(UnitStats.DefaultJumpVelocity * (100f + UnitStats.JumpVelocity) / 100f);
        SoundEffect.AudioSpawn(Jump_Sfx);
        if (amountOfJumpLeft < player.playerData.amountOfJumps)
        {
            player.Core.CoreEffectManager.StartEffects(Jump_Effect, CollisionSenses.GroundCenterPos);
            player.Anim.SetBool("JumpFlip", true);
        }
        if (player.PrimaryAttackState.weapon != null)
        {
            player.PrimaryAttackState.weapon.ChangeActionCounter(0);
        }

        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump() => (amountOfJumpLeft > 0);

    public void ResetAmountOfJumpsLeft()
    {
        amountOfJumpLeft = player.playerData.amountOfJumps;
        player.Anim.SetBool("JumpFlip", false);
    }

    public void DecreaseAmountOfJumpsLeft()
    {
        amountOfJumpLeft--;
    }
}

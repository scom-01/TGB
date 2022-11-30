using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerAbilityState
{
    public PlayerAirAttackState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(player.CheckIfGrounded())
        {
            isAbilityDone = true;
            return;
        }

        player.RB.gravityScale = 0f;
        player.InputHandler.UseSkill1Input();
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (player.CheckIfGrounded())
        {
            Debug.Log("Grounded!");
            player.RB.gravityScale = 5f;
            isAbilityDone = true;
        }

        base.LogicUpdate();
        player.SetVelocityZero();

        
    }

    public void ComboCheck()
    {
        player.RB.gravityScale = 5f;
        isAbilityDone = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAirAttackState : PlayerAbilityState
{
    public bool CanAirAttack = true;

    public PlayerAirAttackState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        CanAirAttack = false;
        if (player.Core.CollisionSenses.CheckIfGrounded)
        {
            isAbilityDone = true;
            return;
        }

        //player.RB.gravityScale = 0f;
        player.InputHandler.UseSkill1Input();
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        //player.SetVelocityZero();

        if(player.InputHandler.DashInput)
        {
            FSM.ChangeState(player.DashState);
            return;
        }

        if (isGrounded)
        {
            Debug.Log("Grounded!");
            //player.RB.gravityScale = 5f;
            AnimationFinishTrigger();
        }
        else
        {
            player.Core.Movement.CheckIfShouldFlip(player.InputHandler.NormInputX);
            player.Core.Movement.SetVelocityX(playerData.movementVelocity * player.InputHandler.NormInputX);
            player.Anim.SetFloat("yVelocity", Mathf.Clamp(player.Core.Movement.CurrentVelocity.y, -3, 13));
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.Core.Movement.CurrentVelocity.x));
        }
        
    }

    public void ComboCheck()
    {
        player.RB.gravityScale = 5f;
        AnimationFinishTrigger();
    }
}

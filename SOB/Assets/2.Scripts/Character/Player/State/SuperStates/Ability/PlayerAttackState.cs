using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public bool CanAttack { get; private set; }
    public PlayerAttackState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanAttack = false;
        
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
        
        if (player.InputHandler.DashInput)
        {
            FSM.ChangeState(player.DashState);
            return;
        }

        player.SetVelocityX(0f);
    }

    public void ComboCheck()
    {
        EndAbility();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            if(xInput != 0f)
            {
                FSM.ChangeState(player.MoveState);
            }
            else if(isAnimationFinished)
            {
                FSM.ChangeState(player.IdleState);
            }
        }
    }
}

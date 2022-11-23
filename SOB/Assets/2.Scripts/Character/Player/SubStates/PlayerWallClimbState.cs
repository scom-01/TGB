using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;

        player.SetVelocityY(playerData.wallClimbVelocity);
        
        if(yInput != 1 && grabInput)//&& !isExitingState)
        {
            //FSM.ChangeState(player.WallGrabState);
        }
        else if(yInput != 1 || !grabInput)
        {
            FSM.ChangeState(player.WallSlideState);
        }
    }
}

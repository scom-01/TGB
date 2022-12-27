using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;

        player.Core.Movement.SetVelocityY(-playerData.wallSlideVelocity);

        /*if (grabInput && yInput >= 0 && !isExitingState)
        {
            FSM.ChangeState(player.WallGrabState);
        }*/
    }
}

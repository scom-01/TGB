using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;

        player.Core.Movement.SetVelocityY(-player.playerData.wallSlideVelocity);

        /*if (grabInput && yInput >= 0 && !isExitingState)
        {
            FSM.ChangeState(player.WallGrabState);
        }*/
    }
}

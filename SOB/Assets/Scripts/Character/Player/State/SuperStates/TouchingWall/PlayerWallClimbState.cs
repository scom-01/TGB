using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;

        Movement.SetVelocityY(player.playerData.wallClimbVelocity);
        
        if(yInput != 1 && grabInput)//&& !isExitingState)
        {
            //FSM.ChangeState(player.WallGrabState);
        }
        else if(yInput != 1 || !grabInput)
        {
            player.FSM.ChangeState(player.WallSlideState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;
        
        if(xInput != 0f)
        {
            player.FSM.ChangeState(player.MoveState);
        }
        else if(isAnimationFinished)
        {
            player.FSM.ChangeState(player.IdleState);
        }
    }
}

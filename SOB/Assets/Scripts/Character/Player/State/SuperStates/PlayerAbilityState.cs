using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;

    public PlayerAbilityState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if(CollisionSenses)
            isGrounded = CollisionSenses.CheckIfGrounded || CollisionSenses.CheckIfPlatform;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && Movement.CurrentVelocity.y <= 0.01f)
            {
                if (player.FSM.OldState == player.InAirState || player.FSM.OldState == player.JumpState)
                {
                    player.FSM.ChangeState(player.LandState);
                }
                else
                {
                    player.FSM.ChangeState(player.IdleState);
                }
            }
            else
            {
                player.FSM.ChangeState(player.InAirState);
            }
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
}

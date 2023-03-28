using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    private GameObject Land_Effect;
    public PlayerLandState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        if (Land_Effect == null)
        {
            Land_Effect = Resources.Load<GameObject>("Prefabs/Particle/Landing_Smoke");
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Core.GetCoreComponent<ParticleManager>().StartParticles(Land_Effect, CollisionSenses.GroundCheck.position);
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

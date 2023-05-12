using Mono.Cecil;
using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    private GameObject Land_Effect;
    private AudioClip Land_SFX;
    public PlayerLandState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        if (Land_Effect == null)
        {
            Land_Effect = Resources.Load<GameObject>("Prefabs/Particle/Landing_Smoke");
        }

        if (Land_SFX == null) 
        {
            Land_SFX = Resources.Load<AudioClip>("Sounds/Effects/SFX_Player_Land_1");
        }
    }

    public override void Enter()
    {
        base.Enter();
        SoundEffect.AudioSpawn(Land_SFX);
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

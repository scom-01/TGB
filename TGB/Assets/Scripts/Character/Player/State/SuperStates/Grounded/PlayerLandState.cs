using Mono.Cecil;
using TGB.CoreSystem;
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
            Land_Effect = Resources.Load<GameObject>("Prefabs/Effects/Landing_Smoke");
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
        player.Core.CoreEffectManager.StartEffects(Land_Effect, CollisionSenses.GroundCenterPos);
        //Land 시 
        for (int i = 0; i < player.InputHandler.ActionInputDelayCheck.Length; i++)
        {
            player.InputHandler.ActionInputDelayCheck[i] = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isExitingState) return;

        if (xInput != 0f)
        {
            player.FSM.ChangeState(player.MoveState);
        }
        else if (isAnimationFinished)
        {
            player.FSM.ChangeState(player.IdleState);
        }
    }
}

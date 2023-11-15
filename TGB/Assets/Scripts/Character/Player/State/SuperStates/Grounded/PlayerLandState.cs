using TGB.CoreSystem;
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
        //착지 시 커맨드 리스트 초기화
        player.Inventory.Weapon.ResetActionCounter();
        SoundEffect.AudioSpawn(Land_SFX);
        player.Core.CoreEffectManager.StartEffectsPos(Land_Effect, CollisionSenses.GroundCenterPos, Land_Effect.transform.localScale);
        Debug.Log("player.InputHandler.ActionInputDelayCheck[i] true");
        //Land 시 
        for (int i = 0; i < player.InputHandler.ActionInputDelayCheck.Length; i++)
        {
            player.InputHandler.ActionInputDelayCheck[i] = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
        {
            player.Core.CoreMovement.SetVelocityY(0);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

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

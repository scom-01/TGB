using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerAbilityState
{
    public bool CanBlock { get; private set; }
    private float lastBlockTime;
    private bool IsGrounded = true;

    public PlayerBlockState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanBlock = false;
        player.InputHandler.UseBlockInput();
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVelocityX(0f);

            if (CheckHit())
            {
                //Block Effect
                Debug.Log("Block!");
                player.KnockBack(1);
            }

            if (Time.time >= startTime + playerData.blockTime)
            {
                isAbilityDone = true;
                lastBlockTime = Time.time;
            }
        }


    }

    private bool CheckHit()
    {
        return Physics2D.OverlapBox(player.gameObject.transform.position + new Vector3(player.BC2D.size.x, 0, 0) * player.FancingDirection, player.BC2D.size, 0f, playerData.enemyAttackMask);
    }

    public bool CheckIfCanBlock()
    {
        return CanBlock && Time.time >= lastBlockTime + playerData.blockCooldown;
    }
    public void ResetCanBlock() => CanBlock = true;
}

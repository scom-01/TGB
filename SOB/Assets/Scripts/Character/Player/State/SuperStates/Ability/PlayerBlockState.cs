using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerAbilityState
{
    public bool CanBlock { get; private set; }
    private float lastBlockTime;
    //private bool IsGrounded = true;


    public PlayerBlockState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanBlock = false;
        player.InputHandler.UseInput(ref player.InputHandler.BlockInput);
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
            Movement.SetVelocityX(0f);

            if (CheckHit())
            {
                //Block Effect
                Debug.Log("Block!");
                //player.KnockBack(1);
            }

            if (Time.time >= startTime + player.playerData.blockTime)
            {
                isAbilityDone = true;
                lastBlockTime = Time.time;
            }
        }


    }

    private bool CheckHit()
    {
        return Physics2D.OverlapBox(player.gameObject.transform.position + new Vector3(player.CC2D.size.x, 0, 0) * Movement.FancingDirection, player.CC2D.size, 0f, player.playerData.WhatIsEnemyUnit);
    }

    public bool CheckIfCanBlock()
    {
        return CanBlock && Time.time >= lastBlockTime + player.playerData.blockCooldown;
    }
    public void ResetCanBlock() => CanBlock = true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    public int DashCount { get; private set; }
    private float lastDashTime;

    private Vector2 lastAIPos;
    public PlayerDashState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();
        player.SetVelocityY(0f);
        player.RB.gravityScale = 0f;
        DecreaseDashCount();
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
            //player.RB.drag = playerData.drag;
            player.SetVelocityX(playerData.dashVelocity * player.FancingDirection);
            CheckIfShouldPlaceAfterImage();
            //if(Time.time >= startTime + playerData.dashti)
            if (Time.time >= startTime + playerData.dashTime)
            {
                if(DashCount>0)
                {
                    CanDash = true;
                }

                player.RB.gravityScale = 5f;
                player.SetVelocityX(0f);
                isAbilityDone = true;
                lastDashTime = Time.time;
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        lastAIPos = player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown && DashCount > 0;
    }

    public void DecreaseDashCount() => DashCount--;

    public void ResetCanDash()
    {
        CanDash = true;
        DashCount = playerData.dashCount;
    }

}

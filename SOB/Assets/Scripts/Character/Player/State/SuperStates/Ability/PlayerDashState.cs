using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    public int DashCount { get; private set; }
    private float lastDashTime;
    private bool IsGrounded = true;

    private Vector2 lastAIPos;
    public PlayerDashState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsGrounded = player.Core.CollisionSenses.CheckIfGrounded;
        if (IsGrounded)
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(playerData.dashColliderHeight);
        }
        else
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(playerData.dashColliderHeight, false);            
        }
        

        CanDash = false;
        player.InputHandler.UseDashInput();
        player.Core.Movement.SetVelocityY(0f);
        player.RB.gravityScale = 0f;
        DecreaseDashCount();
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = 5f;

        if (IsGrounded)
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(playerData.standColliderHeight);
        }
        else
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(playerData.standColliderHeight, false);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.Core.Movement.SetVelocityX(playerData.dashVelocity * player.Core.Movement.FancingDirection);
            player.Core.Movement.SetVelocityY(0);
            
            CheckIfShouldPlaceAfterImage();

            if (Time.time >= startTime + playerData.dashTime)
            {
                if(DashCount > 0)
                {
                    CanDash = true;
                }

                player.Core.Movement.SetVelocityX(0f);
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
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }

    public bool CheckIfCanDash()
    {

        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown && DashCount > 0;
    }

    public void DecreaseDashCount() => DashCount--;
    public bool CheckIfResetDash()
    {
        return Time.time >= lastDashTime + playerData.dashResetCooldown;
    }
    public void ResetCanDash() => CanDash = true;        

    public void ResetDash(int count) => DashCount = count;

}

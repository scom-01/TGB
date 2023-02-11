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
    
    public PlayerDashState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsGrounded = CollisionSenses.CheckIfGrounded;
        if (IsGrounded)
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(player.playerData.dashColliderHeight);
        }
        else
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(player.playerData.dashColliderHeight, false);            
        }
        

        CanDash = false;
        player.InputHandler.UseInput(ref player.InputHandler.DashInput);
        Movement.SetVelocityY(0f);
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
            player.SetColliderHeight(player.playerData.standColliderHeight);
        }
        else
        {
            //콜라이더 크기 변경
            player.SetColliderHeight(player.playerData.standColliderHeight, false);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Movement.SetVelocityX(player.playerData.dashVelocity * Movement.FancingDirection);
            Movement.SetVelocityY(0);
            
            CheckIfShouldPlaceAfterImage();

            if (player.InputHandler.ActionInputs[(int)CombatInputs.primary])
            {
                player.PrimaryAttackState.SetWeapon(player.Inventory.weapons[(int)CombatInputs.primary]);
                player.FSM.ChangeState(player.PrimaryAttackState);
            }
            else if (player.InputHandler.ActionInputs[(int)CombatInputs.secondary])
            {
                player.SecondaryAttackState.SetWeapon(player.Inventory.weapons[(int)CombatInputs.secondary]);
                player.FSM.ChangeState(player.SecondaryAttackState);
            }

            //대쉬 지속시간 종료
            if (Time.time >= startTime + player.playerData.dashDuration)
            {
                if (DashCount > 0)
                {
                    CanDash = true;
                }
                if (player.InputHandler.DashInput)
                {            
                    lastDashTime = Time.time;
                    if(player.DashState.CheckIfCanDash())
                    {
                        Movement.CheckIfShouldFlip(player.InputHandler.NormInputX);
                        player.FSM.ChangeState(player.DashState);
                    }
                }

                Movement.SetVelocityZero();
                if(Time.time>=startTime + player.playerData.dashDuration + player.playerData.dashFlightDuration)
                {
                    isAbilityDone = true;
                    lastDashTime = Time.time;                    
                    Debug.Log($"Dash EndTime = {lastDashTime}");
                }
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, lastAIPos) >= player.playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }

    public bool CheckIfCanDash() => CanDash && Time.time >= lastDashTime + player.playerData.dashCooldown && DashCount > 0;
    public void DecreaseDashCount() => DashCount--;
    public bool CheckIfResetDash()
    {
        return Time.time >= lastDashTime + player.playerData.dashResetCooldown;
    }
    public void ResetCanDash() => CanDash = true;        

    public void ResetDash(int count) => DashCount = count;

}

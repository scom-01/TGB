using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInputHandler;

public class PlayerWeaponState : PlayerAbilityState
{
    public bool CanAttack { get; private set; }

    private Weapon weapon;

    private int xInput;
    private bool JumpInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    public PlayerWeaponState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.InAir = !player.Core.CollisionSenses.CheckIfGrounded;
        weapon.EnterWeapon();

        CanAttack = false;
        
        player.InputHandler.UseSkill1Input();
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        weapon.ExitWeapon();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        
        if (JumpInput && player.JumpState.CanJump() && weapon.weaponData.CanJump)
        {
            player.FSM.ChangeState(player.JumpState);
        }

        //���߿��� ���� �� ��������
        if (weapon.InAir&& player.Core.CollisionSenses.CheckIfGrounded)
        {
            player.FSM.ChangeState(player.LandState);
            return;
        }

        if(shouldCheckFlip)
        {
            player.Core.Movement.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            player.Core.Movement.SetVelocityX(velocityToSet * player.Core.Movement.FancingDirection);
        }

        if (player.InputHandler.DashInput && player.DashState.CheckIfCanDash())
        {
            player.FSM.ChangeState(player.DashState);
            return;
        }

        //player.SetVelocityX(0f);
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
        this.weapon.GetComponentInParent<WeaponManager>().weapon = weapon;
    }

    public void SetPlayerVelocity(float velocity)
    {
        player.Core.Movement.SetVelocityX(velocity * player.Core.Movement.FancingDirection);
        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }
    #region Animation Triggers

    #endregion
}

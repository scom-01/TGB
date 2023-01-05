using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponState : PlayerAbilityState
{
    public bool CanAttack { get; private set; }

    private Weapon weapon;

    private int xInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    public PlayerWeaponState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
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
        
        //공중에서 공격 후 착지상태
        if(weapon.InAir&& player.Core.CollisionSenses.CheckIfGrounded)
        {
            FSM.ChangeState(player.LandState);
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

        if (player.InputHandler.DashInput)
        {
            FSM.ChangeState(player.DashState);
            return;
        }

        //player.SetVelocityX(0f);
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
        if(weapon.GetComponent<AggressiveWeapon>() != null)
        {
            this.weapon.GetComponentInParent<WeaponManager>().aggressiveweapon = (AggressiveWeapon)weapon;
        }
        else
        {
            this.weapon.GetComponentInParent<WeaponManager>().defensiveweapon = (DefensiveWeapon)weapon;
        }
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

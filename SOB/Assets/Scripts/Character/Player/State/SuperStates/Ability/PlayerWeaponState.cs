using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInputHandler;
using SOB.Weapons;
using SOB.CoreSystem;

public class PlayerWeaponState : PlayerAbilityState
{
    public bool CanAttack { get; private set; }

    private Weapon weapon;

    private int xInput;
    private bool JumpInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    public PlayerWeaponState(Unit unit, string animBoolName, Weapon weapon) : base(unit, animBoolName)
    {
        this.weapon = weapon;
        this.weapon.OnExit += ExitHandler;
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

    private void ExitHandler()
    {
        AnimationFinishTrigger();
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;


        //TODO: 공격중 공중으로 전환 시 Anim의 "attack" Param이 제대로 false되지 않는 문제
        /*if (JumpInput && player.JumpState.CanJump() && weapon.weaponData.CanJump)
        {
            player.FSM.ChangeState(player.JumpState);            
        }*/

        //공중에서 공격 후 착지상태
        if (!player.InputHandler.AttackInputs[(int)CombatInputs.primary]&& weapon.InAir&& player.Core.CollisionSenses.CheckIfGrounded)
        {
            player.FSM.ChangeState(player.LandState);
            return;
        }

        if(shouldCheckFlip)
        {
            Movement.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            Movement.SetVelocityX(velocityToSet * Movement.FancingDirection);
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
        //this.weapon.SetCore(this.weapon.WeaponCore);
        //this.weapon.GetComponentInParent<WeaponManager>().weapon = weapon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;
using SOB.CoreSystem;
using SOB.Weapons.Components;

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

        Debug.Log($"Enter input = {input}");
        player.InputHandler.UseInput(ref input);
        Debug.Log($"input = {input}");
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


        if (JumpInput && player.JumpState.CanJump() && weapon.weaponData.CanJump)
        {
            weapon.EventHandler.AnimationFinishedTrigger();
            player.FSM.ChangeState(player.JumpState);
            return;
        }

        //공중에서 공격 후 착지상태
        //TODO:Input의 boolean값을 가져와서 판별하는 방법으로 변경해야 할 듯 하다 ex)AttackInputs
        if (!player.InputHandler.ActionInputs[(int)CombatInputs.primary] && weapon.InAir && player.Core.CollisionSenses.CheckIfGrounded)
        {
            weapon.EventHandler.AnimationFinishedTrigger();
            player.FSM.ChangeState(player.LandState);
            return;
        }

        shouldCheckFlip = weapon.weaponData.GetData<MovementData>().ActionData[weapon.CurrentActionCounter].CanFlip;
        if (shouldCheckFlip)
        {
            Movement.CheckIfShouldFlip(xInput);
        }

        setVelocity = weapon.weaponData.GetData<MovementData>().ActionData[weapon.CurrentActionCounter].CanMoveCtrl;
        if (setVelocity)
        {
            //Movement.SetVelocityX(velocityToSet * Movement.FancingDirection);
        Movement.SetVelocityX(player.playerData.commonStats.movementVelocity * xInput);
        }


        if (player.InputHandler.DashInput && player.DashState.CheckIfCanDash())
        {            
            weapon.EventHandler.AnimationFinishedTrigger();
            player.FSM.ChangeState(player.DashState);
            return;
        }

        //player.SetVelocityX(0f);
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;

        weapon.InitializeWeapon(this, player.Core);
        //this.weapon.SetCore(this.weapon.WeaponCore);
        //this.weapon.GetComponentInParent<WeaponManager>().weapon = weapon;
    }
}

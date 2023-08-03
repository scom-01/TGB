using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;
using SOB.CoreSystem;
using SOB.Weapons.Components;
using System.Linq;

public class PlayerWeaponState : PlayerAbilityState
{
    public bool CanAttack { get; private set; }

    public Weapon weapon;

    private int xInput;
    private int yInput;
    private bool JumpInput;
    private bool isPrimary;

    public PlayerWeaponState(Unit unit, string animBoolName, bool primary) : base(unit, animBoolName)
    {
        isPrimary = primary;
    }

    public override void Enter()
    {
        base.Enter();
        this.weapon.OnExit += ExitHandler;
        int idx = 1;
        if (isPrimary)
            idx = 0;
        player.InputHandler.UseInput(ref player.InputHandler.ActionInputs[idx]);
        //setVelocity = false;

        if (isPrimary)
        {
            weapon.Command = CommandEnum.Primary;
        }
        else
        {
            weapon.Command = CommandEnum.Secondary;
        }

        weapon.EnterWeapon();
        CanAttack = false;
        startTime = Time.time;
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        weapon.InAir = !(CollisionSenses.CheckIfGrounded || CollisionSenses.CheckIfPlatform);
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;

        if (JumpInput && player.JumpState.CanJump() && CollisionSenses.CheckIfPlatform && yInput < 0)
        {
            player.StartCoroutine(player.DisableCollision());
            return;
        }
        else if (JumpInput && player.JumpState.CanJump() && !player.CC2D.isTrigger)
        {
            Movement.SetVelocityZero();
            weapon.EventHandler.AnimationFinishedTrigger();
            player.FSM.ChangeState(player.JumpState);
            return;
        }

        //공중에서 공격 후 착지상태
        //TODO:Input의 boolean값을 가져와서 판별하는 방법으로 변경해야 할 듯 하다 ex)AttackInputs
        if (/*!player.InputHandler.ActionInputs[(int)CombatInputs.primary] &&*/ weapon.InAir && CollisionSenses.CheckIfGrounded)
        {
            weapon.EventHandler.AnimationFinishedTrigger();
            player.FSM.ChangeState(player.LandState);
            return;
        }

        //shouldCheckFlip = weapon.weaponData.GetData<MovementData>().ActionData[weapon.CurrentActionCounter].CanFlip;

        if (Movement.CanFlip)
        {
            Movement.CheckIfShouldFlip(xInput);
        }

        //setVelocity = weapon.weaponData.GetData<MovementData>().ActionData[weapon.CurrentActionCounter].CanMoveCtrl;
        if (Movement.CanMovement)
        {
            Movement.SetVelocityX(UnitStats.MoveSpeed * xInput);
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
    }

    public bool CheckCommand
        (ref List<CommandEnum> q)
    {
        CommandEnum command = CommandEnum.Secondary;
        if (isPrimary)
            command = CommandEnum.Primary;
        q.Add(command);
        if (!(CollisionSenses.CheckIfGrounded || CollisionSenses.CheckIfPlatform))
        {
            if (CalCommand(weapon.weaponData.weaponCommandDataSO.AirCommandList, q))
            {
                return true;
            }
        }
        else
        {
            if(CalCommand(weapon.weaponData.weaponCommandDataSO.GroundedCommandList, q))
            {
                return true;
            }
        }
        weapon.ChangeActionCounter(0);
        return false;
    }

    private bool CalCommand(List<CommandList> commandLists, List<CommandEnum> q)
    {
        for (int i = 0; i < commandLists.Count; i++)
        {
            bool pass = true;
            for (int j = 0; j < q.Count; j++)
            {
                if (commandLists[i].commands.Count < j + 1 ||
                    commandLists[i].commands[j].command != q[j]
                    )
                {
                    pass = false;
                    break;
                }
                if (commandLists[i].commands[j].animOC == null)
                {
                    weapon.oc = weapon.weaponData.weaponCommandDataSO.DefaultAnimator;
                    weapon.weaponGenerator.GenerateWeapon(commandLists[i].commands[j].data);
                }
                else
                {
                    weapon.weaponGenerator.GenerateWeapon(commandLists[i].commands[j]);
                }
            }
            if (pass)
            {
                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
}

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

        weapon.InAir = !CollisionSenses.CheckIfGrounded;
        if(isPrimary)
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

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;

        if (JumpInput && player.JumpState.CanJump() && weapon.weaponData.CanJump && CollisionSenses.CheckIfPlatform && yInput < 0)
        {
            player.StartCoroutine(player.DisableCollision());
            return;
        }
        else if (JumpInput && player.JumpState.CanJump() && weapon.weaponData.CanJump && !player.BC2D.isTrigger)
        {
            weapon.EventHandler.AnimationFinishedTrigger();
            player.FSM.ChangeState(player.JumpState);
            return;
        }

        //공중에서 공격 후 착지상태
        //TODO:Input의 boolean값을 가져와서 판별하는 방법으로 변경해야 할 듯 하다 ex)AttackInputs
        if (!player.InputHandler.ActionInputs[(int)CombatInputs.primary] && weapon.InAir && CollisionSenses.CheckIfGrounded)
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
            Movement.SetVelocityX(UnitStats.StatsData.MovementVelocity * xInput);
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

    public bool CheckCommand(ref List<CommandEnum> q)
    {
        CommandEnum command = CommandEnum.Secondary;
        if (isPrimary)
            command = CommandEnum.Primary;
        q.Add(command);
        if (!CollisionSenses.CheckIfGrounded)
        {
            for (int i = 0; i < weapon.weaponData.AirCommandList.Count; i++)
            {
                bool pass = true;
                for (int j = 0; j < q.Count; j++)
                {
                    if (weapon.weaponData.AirCommandList[i].commands.Count < j + 1 ||
                        weapon.weaponData.AirCommandList[i].commands[j] != q[j]
                        )
                    {
                        pass = false;
                        break;
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
        }
        else
        {
            for (int i = 0; i < weapon.weaponData.GroundedCommandList.Count; i++)
            {
                bool pass = true;
                for (int j = 0;j < q.Count;j++)
                {
                    if (weapon.weaponData.GroundedCommandList[i].commands.Count < j + 1||
                        weapon.weaponData.GroundedCommandList[i].commands[j] != q[j]
                        )
                    {
                        pass = false;
                        break;
                    }
                }
                if(pass)
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
        }
        weapon.ResetActionCounter();
        q.Clear();
        return false;
    }
}

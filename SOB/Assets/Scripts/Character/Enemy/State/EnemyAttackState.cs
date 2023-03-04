using SOB.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAttackState : EnemyState
{
    private Weapon weapon;

    public EnemyAttackState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        this.weapon.OnExit += ExitHandler;

        weapon.InAir = !CollisionSenses.CheckIfGrounded;
        weapon.EnterWeapon();

        startTime = Time.time;
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this, unit.Core);
    }
}

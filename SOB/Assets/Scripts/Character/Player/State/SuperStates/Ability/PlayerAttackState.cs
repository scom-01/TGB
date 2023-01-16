using SOB.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    public PlayerAttackState(Unit unit, string animBoolName, Weapon weapon) : base(unit, animBoolName)
    {
        this.weapon = weapon;
        //weapon.OnExit += ExitHanlder;
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}

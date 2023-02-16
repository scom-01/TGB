using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDeathState : PlayerAbilityState
{
    public PlayerDeathState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
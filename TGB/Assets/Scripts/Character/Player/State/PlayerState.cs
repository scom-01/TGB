using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : UnitState
{
    protected Player player;

    public bool input = false;
    public void SetInput(ref bool input) => this.input = input;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Death.isDead)
            return;
    }

    public PlayerState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        player = unit as Player;
        this.animBoolName = animBoolName;
    }
}

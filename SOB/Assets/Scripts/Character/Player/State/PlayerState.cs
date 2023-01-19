using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : UnitState
{
    protected Player player;

    public bool input = false;
    protected Movement Movement
    {
        get => movement ?? player.Core.GetCoreComponent(ref movement);
    }
    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? player.Core.GetCoreComponent(ref collisionSenses);
    }

    public void SetInput(ref bool input) => this.input = input;


    private Movement movement;
    private CollisionSenses collisionSenses;

    public PlayerState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        player = unit as Player;
        this.animBoolName = animBoolName;
    }
}

using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : UnitState
{
    protected Enemy enemy;

    protected EnemyMovement Movement
    {
        get => movement as EnemyMovement ?? enemy.core.GetCoreComponent(ref movement) as EnemyMovement;
    }
    protected EnemyCollisionSenses CollisionSenses
    {
        get => collisionSenses as EnemyCollisionSenses ?? enemy.core.GetCoreComponent(ref collisionSenses) as EnemyCollisionSenses;
    }

    private EnemyMovement movement;
    private EnemyCollisionSenses collisionSenses;
    public EnemyState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy = unit as Enemy;
        this.animBoolName = animBoolName;
    }
}

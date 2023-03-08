using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1_MoveState : EnemyRunState
{
    private Enemy_Melee1 enemy_Melee;
    public Enemy_Melee1_MoveState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        this.enemy_Melee = enemy as Enemy_Melee1;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy_Melee1_MoveState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (CollisionSenses.UnitDectected && unit.Inventory.weapons.Count > 0)
        {
            Enemy_Attack();
        }

    }

    public override void Enemy_Attack()
    {
        enemy_Melee.AttackState.SetWeapon(unit.Inventory.weapons[0]);
        unit.FSM.ChangeState(enemy_Melee.AttackState);
        Debug.LogWarning("ChanegEnemy_Melee1");
    }
}

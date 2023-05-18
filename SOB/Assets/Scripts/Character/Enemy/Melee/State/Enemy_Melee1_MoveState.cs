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

    public override void Enemy_Attack()
    {
        enemy_Melee.AttackState.SetWeapon(unit.Inventory.Weapon);
        unit.FSM.ChangeState(enemy_Melee.AttackState);
        Debug.LogWarning("ChanegEnemy_Melee1");
    }
}

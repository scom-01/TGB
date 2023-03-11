using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1_AttackState : EnemyAttackState
{
    private Enemy_Melee1 enemy_Melee1;

    public Enemy_Melee1_AttackState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
    }
    public override void IdleState()
    {
        enemy.FSM.ChangeState(enemy_Melee1.IdleState);
    }
}

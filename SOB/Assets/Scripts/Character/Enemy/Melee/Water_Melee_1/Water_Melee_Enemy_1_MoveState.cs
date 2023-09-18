using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Melee_Enemy_1_MoveState : EnemyRunState
{
    private Water_Melee_Enemy_1 enemy_Melee;
    public Water_Melee_Enemy_1_MoveState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        this.enemy_Melee = enemy as Water_Melee_Enemy_1;
    }

    public override void Enemy_Attack()
    {        
        enemy_Melee.FSM.ChangeState(enemy_Melee.IdleState);
    }

    public override void IdleState()
    {
        unit.FSM.ChangeState(enemy_Melee.IdleState);
    }
}

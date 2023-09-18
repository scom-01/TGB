using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water_Melee_Enemy_1_HitState : EnemyHitState
{
    private Water_Melee_Enemy_1 enemy_Melee1;
    public Water_Melee_Enemy_1_HitState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Water_Melee_Enemy_1;
    }

    public override void IdleState()
    {
        unit.FSM.ChangeState(enemy_Melee1.IdleState);
    }
}
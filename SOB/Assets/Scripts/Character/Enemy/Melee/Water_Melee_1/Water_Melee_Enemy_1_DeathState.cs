using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water_Melee_Enemy_1_DeathState : EnemyDeathState
{
    private Water_Melee_Enemy_1 enemy_Melee1;
    public Water_Melee_Enemy_1_DeathState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Water_Melee_Enemy_1;
    }

}

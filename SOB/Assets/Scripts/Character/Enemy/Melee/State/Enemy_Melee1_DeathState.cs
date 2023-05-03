using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Melee1_DeathState : EnemyDeathState
{
    private Enemy_Melee1 enemy_Melee1;
    public Enemy_Melee1_DeathState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
    }

}

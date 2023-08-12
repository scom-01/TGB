using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1_IdleState : EnemyIdleState
{
    private Enemy_Melee1 enemy_Melee1;

    public Enemy_Melee1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
    }

    public override void Pattern()
    {
        //공격 대상 판단
        if (enemy_Melee1.TargetUnit == null)
            return;

        //타겟 방향 회전
        FlipToTarget();

        if (!isDelayCheck)
            return;

        isDelayCheck = false;

        enemy_Melee1.AttackState.SetWeapon(unit.Inventory.Weapon);
        if ((enemy_Melee1.TargetUnit.transform.position - enemy_Melee1.transform.position).magnitude <= enemy_Melee1.enemyData.UnitDetectedDistance)
        {
            unit.FSM.ChangeState(enemy_Melee1.RunState);
        }
        else
        {
            //타겟 초기화
            enemy_Melee1.SetTarget(null);
        }
    }
}

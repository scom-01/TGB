using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Melee1_HitState : EnemyHitState
{
    private Enemy_Melee1 enemy_Melee1;
    public Enemy_Melee1_HitState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
    }

    public override void IdleState()
    {
        unit.FSM.ChangeState(enemy_Melee1.IdleState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= unit.UnitData.knockBackDuration + startTime)
        {
            unit.Anim.speed = 1.0f;
            unit.Core.GetCoreComponent<DamageReceiver>().isHit = false;
            IdleState();
        }
    }
}
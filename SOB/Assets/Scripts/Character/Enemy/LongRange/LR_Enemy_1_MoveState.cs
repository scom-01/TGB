using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Enemy_1_MoveState : EnemyRunState
{
    private LR_Enemy_1 enemy_LR;
    public LR_Enemy_1_MoveState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        this.enemy_LR = enemy as LR_Enemy_1;
    }
    public override void Pattern()
    {
        enemy_LR.EnemyPattern();
    }
    public override void IdleState()
    {
        unit.FSM.ChangeState(enemy_LR.IdleState);
    }
}

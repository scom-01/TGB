using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss_Stage_1_MoveState : EnemyRunState
{
    private Boss_Static_Stage_1 boss_Static_Stage_1;
    public MiddleBoss_Stage_1_MoveState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        this.boss_Static_Stage_1 = enemy as Boss_Static_Stage_1;
    }

    public override void Enemy_Attack()
    {
        boss_Static_Stage_1.AttackState.SetWeapon(unit.Inventory.Weapon);
        unit.FSM.ChangeState(boss_Static_Stage_1.AttackState);
    }

    public override void IdleState()
    {
        unit.FSM.ChangeState(boss_Static_Stage_1.IdleState);
    }
}

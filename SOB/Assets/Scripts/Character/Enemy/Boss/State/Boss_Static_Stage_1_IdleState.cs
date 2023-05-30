using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Static_Stage_1_IdleState : EnemyIdleState
{
    private Boss_Static_Stage_1 boss_Static_Stage_1;
    public Boss_Static_Stage_1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        boss_Static_Stage_1 = enemy as Boss_Static_Stage_1;
    }
    public override void ChangeState()
    {
        if (boss_Static_Stage_1.TargetUnit == null)
            return;

        if ((boss_Static_Stage_1.TargetUnit.transform.position - boss_Static_Stage_1.transform.position).magnitude < boss_Static_Stage_1.enemyData.UnitDetectedDistance)
        {
            Attack();
        }
        else
        {
            Teleport();
        }

        //unit.FSM.ChangeState(boss_Static_Stage_1.RunState);
    }

    private void Teleport()
    {
        boss_Static_Stage_1.TeleportState.SetWeapon(unit.Inventory.Weapon);
        unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[0]);
        unit.FSM.ChangeState(boss_Static_Stage_1.TeleportState);
    }

    private void Attack()
    {
        boss_Static_Stage_1.AttackState.SetWeapon(unit.Inventory.Weapon);
        unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[0].commands[0]);
        unit.FSM.ChangeState(boss_Static_Stage_1.AttackState);
    }
}
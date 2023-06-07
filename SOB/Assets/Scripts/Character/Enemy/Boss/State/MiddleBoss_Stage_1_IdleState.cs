using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss_Stage_1_IdleState : EnemyIdleState
{
    private MiddleBoss_Stage_1 MiddleBoss_Stage_1;
    public MiddleBoss_Stage_1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        MiddleBoss_Stage_1 = enemy as MiddleBoss_Stage_1;
    }
    public override void ChangeState()
    {
        if (MiddleBoss_Stage_1.TargetUnit == null)
            return;

        if ((MiddleBoss_Stage_1.TargetUnit.transform.position - MiddleBoss_Stage_1.transform.position).magnitude < MiddleBoss_Stage_1.enemyData.UnitDetectedDistance)
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
        MiddleBoss_Stage_1.TeleportState.SetWeapon(unit.Inventory.Weapon);
        unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.AirCommandList[0].commands[0]);
        unit.FSM.ChangeState(MiddleBoss_Stage_1.TeleportState);
    }

    private void Attack()
    {
        MiddleBoss_Stage_1.AttackState.SetWeapon(unit.Inventory.Weapon);
        var max = unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList.Count;
        unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[Random.Range(0, max)].commands[0]);
        unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss_Stage_1_IdleState : EnemyIdleState
{
    private MiddleBoss_Stage_1 MiddleBoss_Stage_1;

    private List<bool> Phase = new List<bool>() { false, false, false };
    public MiddleBoss_Stage_1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        MiddleBoss_Stage_1 = enemy as MiddleBoss_Stage_1;
    }
    public override void ChangeState()
    {
        if (MiddleBoss_Stage_1.TargetUnit == null)
            return;

        Pattern();
        //if ((MiddleBoss_Stage_1.TargetUnit.transform.position - MiddleBoss_Stage_1.transform.position).magnitude < MiddleBoss_Stage_1.enemyData.UnitDetectedDistance)
        //{
        //    Attack();
        //}
        //else
        //{
        //    Teleport();
        //}

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

    private void Pattern()
    {
        MiddleBoss_Stage_1.AttackState.SetWeapon(unit.Inventory.Weapon);

        //현재 체력 50% ~ 100%
        if (unit.Core.CoreUnitStats.CurrentHealth >= unit.Core.CoreUnitStats.MaxHealth / 2f)
        {
            Phase[0] = true;
            var max = unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList.Count;
            unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[0]);
            unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
        }
        //현재 체력 20% ~ 49%
        else if (unit.Core.CoreUnitStats.CurrentHealth >= unit.Core.CoreUnitStats.MaxHealth / 5f)
        {            
            //페이즈당 한 번 실행
            if (!Phase[1])
            {
                if (GameManager.Inst?.StageManager.GetType() == typeof(BossStageManager))
                {
                    unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.AirCommandList[0].commands[1]);
                    unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                    var boss_stage = GameManager.Inst?.StageManager as BossStageManager;
                    boss_stage.PlayPattern(boss_stage.Pattern[0]);
                }
                Phase[1] = true;
                return;
            }            
        }
        //현재 체력 0 ~ 19%
        else
        {
            //페이즈당 한 번 실행
            if (!Phase[2])
            {

                Phase[2] = true;
                return;
            }
        }
    }
}

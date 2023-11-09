using System.Collections.Generic;

public class MiddleBoss_Stage_1_IdleState : EnemyIdleState
{
    private MiddleBoss_Stage_1 MiddleBoss_Stage_1;

    private List<bool> Phase = new List<bool>() { false, false, false };
    public MiddleBoss_Stage_1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        MiddleBoss_Stage_1 = enemy as MiddleBoss_Stage_1;
    }

    private void Teleport()
    {
        MiddleBoss_Stage_1.TeleportState.SetWeapon(unit.Inventory.Weapon);
        unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.AirCommandList[0].commands[0]);
        unit.FSM.ChangeState(MiddleBoss_Stage_1.TeleportState);
    }

    public override void Pattern()
    {
        MiddleBoss_Stage_1.AttackState.SetWeapon(unit.Inventory.Weapon);
        //현재 체력 50% ~ 100%
        if (unit.Core.CoreUnitStats.CurrentHealth >= unit.Core.CoreUnitStats.MaxHealth / 2f)
        {
            if (!Phase[0])
            {
                Phase[0] = true;
                return;
            }

            if ((MiddleBoss_Stage_1.TargetUnit.transform.position - MiddleBoss_Stage_1.transform.position).magnitude <= MiddleBoss_Stage_1.enemyData.UnitDetectedDistance)
            {
                //투사체
                if (EnemyCollisionSenses.isUnitInFrontDetectedArea)
                {
                    unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[0]);
                    unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                }                
            }
            else
            {
                //unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[2].commands[1]);
                //unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                Teleport();
            }
            
        }
        //현재 체력 20% ~ 49%
        else if (unit.Core.CoreUnitStats.CurrentHealth >= unit.Core.CoreUnitStats.MaxHealth / 5f)
        {            
            //페이즈당 한 번 실행 BloodWave            
            if (!Phase[1])
            {
                if (GameManager.Inst?.StageManager.GetType() == typeof(BossStageManager))
                {
                    unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.AirCommandList[0].commands[1]);
                    unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                    var boss_stage = GameManager.Inst?.StageManager as BossStageManager;
                    boss_stage.PlayPattern(boss_stage?.Pattern[0]);
                }
                Phase[1] = true;
                return;
            }
            CheckPattern();
            //인식 범위 내 
            if ((MiddleBoss_Stage_1.TargetUnit.transform.position - MiddleBoss_Stage_1.transform.position).magnitude <= MiddleBoss_Stage_1.enemyData.UnitDetectedDistance)
            {
                //일직선 상
                if (EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
                {
                    unit.Core.CoreMovement.FlipToTarget();
                    unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[1]);
                    unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                }
                else
                {
                    unit.Core.CoreMovement.FlipToTarget();
                }
            }
            //인식 범위 밖
            else
            {
                unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[2].commands[1]);
                unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
            }
        }
        //현재 체력 0 ~ 19%
        else
        {
            //페이즈당 한 번 실행 BloodWave            
            if (!Phase[2])
            {
                if (GameManager.Inst?.StageManager.GetType() == typeof(BossStageManager))
                {
                    unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.AirCommandList[0].commands[1]);
                    unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                    var boss_stage = GameManager.Inst?.StageManager as BossStageManager;
                    boss_stage.PlayPattern(boss_stage?.Pattern[0]);
                }
                Phase[2] = true;
                return;
            }
            CheckPattern();
            if ((MiddleBoss_Stage_1.TargetUnit.transform.position - MiddleBoss_Stage_1.transform.position).magnitude <= MiddleBoss_Stage_1.enemyData.UnitDetectedDistance)
            {
                if (EnemyCollisionSenses.isUnitInFrontDetectedArea)
                {
                    if (unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands.Count >= 3)
                    {
                        unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[2]);
                        unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
                    }
                }
            }
            else
            {
                unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[2].commands[1]);
                unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
            }
        }
    }

    private void CheckPattern()
    {
        for (int i = 0; i < MiddleBoss_Stage_1.Pattern_Idx.Count; i++)
        {
            if (MiddleBoss_Stage_1.Pattern_Idx[i].Used)
                continue;

            unit.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.AirCommandList[0].commands[1]);
            unit.FSM.ChangeState(MiddleBoss_Stage_1.AttackState);
            var boss_stage = GameManager.Inst?.StageManager as BossStageManager;
            boss_stage.PlayPattern(boss_stage?.Pattern[1]);
            MiddleBoss_Stage_1.Pattern_Idx[i].Used = true;
        }
    }
    public override void MoveState()
    {
    }
}

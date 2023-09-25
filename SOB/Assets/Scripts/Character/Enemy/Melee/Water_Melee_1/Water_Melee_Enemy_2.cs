using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water_Melee_Enemy_2 : Melee_Enemy_1
{
    public override void EnemyPattern()
    {
        if (TargetUnit == null)
            return;

        //인식 범위 내 
        if ((TargetUnit.transform.position - transform.position).magnitude <= enemyData.UnitAttackDistance)
        {
            if (Pattern_Idx.Count == 0)
                return;
            int patternCount = Pattern_Idx.Count;
            for(int i = 0; i < patternCount; i++)
            {
                //패턴 스킵 여부
                if (Pattern_Idx[i].Used)
                    continue;

                if(Pattern_Idx[i].Boundary != 0 && (Core.CoreUnitStats.CurrentHealth / Core.CoreUnitStats.MaxHealth) < Pattern_Idx[i].Boundary)
                {
                    Pattern_Idx[i].Used = true;
                    continue;
                }
                //일직선 상
                if ((Core.CoreCollisionSenses as EnemyCollisionSenses).isUnitInFrontDetectedArea || (Core.CoreCollisionSenses as EnemyCollisionSenses).isUnitInBackDetectedArea)
                {
                    if (Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList.Count > i)
                    {
                        if ((TargetUnit.transform.position - transform.position).magnitude > Pattern_Idx[i].Detected_Distance)
                            continue;

                        if (Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[i].commands[0] == null)
                            break;
                        AttackState.SetWeapon(Inventory.Weapon);
                        RunState.FlipToTarget();
                        Inventory.Weapon.weaponGenerator.GenerateWeapon(Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[i].commands[0]);
                        FSM.ChangeState(AttackState);
                        return;
                    }
                }
                break;
            }
            ////일직선 상
            //if ((Core.CoreCollisionSenses as EnemyCollisionSenses).isUnitInFrontDetectedArea || (Core.CoreCollisionSenses as EnemyCollisionSenses).isUnitInBackDetectedArea)
            //{
            //    AttackState.SetWeapon(Inventory.Weapon);
            //    //근거리
            //    if ((TargetUnit.transform.position - transform.position).magnitude <= 1.5)
            //    {
            //        RunState.FlipToTarget();
            //        Inventory.Weapon.weaponGenerator.GenerateWeapon(Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[0].commands[0]);
            //        FSM.ChangeState(AttackState);
            //        return;
            //    }
            //    //중거리 돌진 공격
            //    RunState.FlipToTarget();
            //    Inventory.Weapon.weaponGenerator.GenerateWeapon(Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[0]);
            //    FSM.ChangeState(AttackState);
            //    return;
            //}
        }
        RunState.FlipToTarget();
        FSM.ChangeState(RunState);
    }
}

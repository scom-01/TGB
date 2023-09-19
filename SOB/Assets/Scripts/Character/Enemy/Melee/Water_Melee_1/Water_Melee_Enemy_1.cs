using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water_Melee_Enemy_1 : Melee_Enemy_1
{
    public override void EnemyPattern()
    {
        if (TargetUnit == null)
            return;

        //인식 범위 내 
        if ((TargetUnit.transform.position - transform.position).magnitude <= enemyData.UnitAttackDistance)
        {
            //일직선 상
            if ((Core.CoreCollisionSenses as EnemyCollisionSenses).isUnitInFrontDetectedArea || (Core.CoreCollisionSenses as EnemyCollisionSenses).isUnitInBackDetectedArea)
            {
                AttackState.SetWeapon(Inventory.Weapon);
                //근거리
                if ((TargetUnit.transform.position - transform.position).magnitude <= 1.5)
                {
                    RunState.FlipToTarget();
                    Inventory.Weapon.weaponGenerator.GenerateWeapon(Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[0].commands[0]);
                    FSM.ChangeState(AttackState);
                    return;
                }
                //중거리 돌진 공격
                RunState.FlipToTarget();
                Inventory.Weapon.weaponGenerator.GenerateWeapon(Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[0]);
                FSM.ChangeState(AttackState);
                return;
            }
        }
        RunState.FlipToTarget();
        FSM.ChangeState(RunState);
    }
}

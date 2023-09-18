using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Melee_Enemy_1_IdleState : EnemyIdleState
{
    private Water_Melee_Enemy_1 enemy_Melee1;

    public Water_Melee_Enemy_1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Water_Melee_Enemy_1;
    }

    public override void Pattern()
    {
        //인식 범위 내 
        if ((enemy_Melee1.TargetUnit.transform.position - enemy_Melee1.transform.position).magnitude <= enemy_Melee1.enemyData.UnitDetectedDistance)
        {
            //일직선 상
            if (EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
            {
                enemy_Melee1.AttackState.SetWeapon(unit.Inventory.Weapon);
                //근거리
                if ((enemy_Melee1.TargetUnit.transform.position - enemy_Melee1.transform.position).magnitude <= 1.5)
                {
                    FlipToTarget();                    
                    enemy_Melee1.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[0].commands[0]);
                    enemy_Melee1.FSM.ChangeState(enemy_Melee1.AttackState);
                    return;
                }
                //중거리 돌진 공격
                FlipToTarget();
                enemy_Melee1.Inventory.Weapon.weaponGenerator.GenerateWeapon(unit.Inventory.Weapon.weaponData.weaponCommandDataSO.GroundedCommandList[1].commands[0]);
                enemy_Melee1.FSM.ChangeState(enemy_Melee1.AttackState);
                return;
            }
        }
        if (Random.Range(0.5f, 1f) >= 0.5f)
        {
            Movement.Flip();
        }
        unit.FSM.ChangeState(enemy_Melee1.RunState);
    }

    public override void MoveState()
    {
        if (Random.Range(0.5f, 1f) >= 0.5f)
        {
            Movement.Flip();
        }
        unit.FSM.ChangeState(enemy_Melee1.RunState);
    }
}

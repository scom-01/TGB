using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1_IdleState : EnemyIdleState
{
    private Enemy_Melee1 enemy_Melee1;

    public Enemy_Melee1_IdleState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy_Melee1 = enemy as Enemy_Melee1;
    }

    public override void Pattern()
    {
        //인식 범위 내 
        if ((enemy_Melee1.TargetUnit.transform.position - enemy_Melee1.transform.position).magnitude <= enemy_Melee1.enemyData.UnitDetectedDistance)
        {
            //일직선 상
            if (EnemyCollisionSenses.isUnitInFrontDetectedArea || EnemyCollisionSenses.isUnitInBackDetectedArea)
            {
                //달려듬
                FlipToTarget();
                enemy_Melee1.AttackState.SetWeapon(unit.Inventory.Weapon);
                unit.FSM.ChangeState(enemy_Melee1.RunState);
                return;
            }
        }
        if (Random.Range(0.5f, 1f) >= 0.5f)
        {
            Movement.Flip();
        }
        unit.FSM.ChangeState(enemy_Melee1.RunState);
        //    else
        //    {
        //        if (Random.Range(0.5f, 1f) >= 0.5f)
        //        {
        //            Movement.Flip();
        //        }
        //        unit.FSM.ChangeState(enemy_Melee1.RunState);
        //    }
        //}
        ////인식 범위 밖
        //else
        //{
        //    if (Random.Range(0.5f, 1f) >= 0.5f)
        //    {
        //        Movement.Flip();
        //    }
        //    unit.FSM.ChangeState(enemy_Melee1.RunState);
        //}
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

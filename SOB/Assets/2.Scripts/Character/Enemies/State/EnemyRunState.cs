using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    public EnemyRunState(Enemy enemy, EnemyFSM fSM, EnemyData enemyData, string animBoolName) : base(enemy, fSM, enemyData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckIfCliff();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //절벽 체크
    public void CheckIfCliff()
    {

    }
}

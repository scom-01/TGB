using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : UnitState
{
    protected bool isAbilityDone;
    protected bool isGrounded;
    protected Enemy enemy;

    protected Movement Movement
    {
        get => movement ?? enemy.Core.GetCoreComponent(ref movement);
    }
    protected EnemyCollisionSenses CollisionSenses
    {
        get => collisionSenses ?? enemy.Core.GetCoreComponent(ref collisionSenses);
    }

    private Movement movement;
    private EnemyCollisionSenses collisionSenses;
    public EnemyState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        enemy = unit as Enemy;
        this.animBoolName = animBoolName;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
            isGrounded = CollisionSenses.CheckIfGrounded;
    }


    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (isAnimationFinished)
        //{
        //    enemy.FSM.ChangeState(enemy.IdleState);
        //}

        //if (enemy.Core.GetCoreComponent<DamageReceiver>().isHit && enemy.FSM.CurrentState != enemy.HitState)
        //{
        //    enemy.FSM.ChangeState(enemy.HitState);
        //    return;
        //}
    }
}

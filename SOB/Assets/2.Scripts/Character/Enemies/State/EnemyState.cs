using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyFSM FSM;
    protected EnemyData enemyData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;
    public EnemyState(Enemy enemy, EnemyFSM fSM, EnemyData enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.FSM = fSM;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
    }

    //State���� �� �������Լ� �������� ȣ��Ǵ� �Լ�
    public virtual void Enter()
    {
        DoChecks();
        enemy.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    //State���� �� ���� �������� ȣ��Ǵ� �Լ�
    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    //Update
    public virtual void LogicUpdate()
    {

    }

    //FixedUpdate
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}

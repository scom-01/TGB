using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerFSM FSM;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerFSM fSM, PlayerData playerData,string animBoolName)
    {
        this.player = player;
        this.FSM = fSM;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    //State���� �� �������Լ� �������� ȣ��Ǵ� �Լ�
    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    //State���� �� ���� �������� ȣ��Ǵ� �Լ�
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
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

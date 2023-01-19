using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerAbilityState
{
    public bool Hold;
    private bool Skill2Input;
    private bool Skill2InputStop;

    public PlayerHeavyAttackState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Hold = true;
        player.InputHandler.UseInput(ref player.InputHandler.Skill2Input);
    }

    public override void Exit()
    {
        base.Exit();
        Hold = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Skill2Input = player.InputHandler.Skill2Input;
        Skill2InputStop = player.InputHandler.Skill2InputStop;

        //player.Core.Movement.SetVelocityZero();
        if(Skill2InputStop)
        {
            if(Hold)
            {
                AnimationFinishTrigger();
            }
        }

        if(isAnimationFinished)
        {
            AnimationFinishTrigger();
        }
    }
}

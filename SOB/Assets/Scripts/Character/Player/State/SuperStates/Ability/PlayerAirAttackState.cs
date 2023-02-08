using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAirAttackState : PlayerAbilityState
{
    public bool CanAirAttack = true;

    public PlayerAirAttackState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        CanAirAttack = false;
        if (CollisionSenses.CheckIfGrounded)
        {
            isAbilityDone = true;
            return;
        }

        //player.RB.gravityScale = 0f;
        player.InputHandler.UseInput(ref player.InputHandler.Skill1Input);
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        //player.SetVelocityZero();

        if(player.InputHandler.DashInput)
        {
            player.FSM.ChangeState(player.DashState);
            return;
        }

        if (isGrounded)
        {
            Debug.Log("Grounded!");
            //player.RB.gravityScale = 5f;
            AnimationFinishTrigger();
        }
        else
        {
            Movement.CheckIfShouldFlip(player.InputHandler.NormInputX);
            Movement.SetVelocityX(player.playerData.commonStats.movementVelocity * player.InputHandler.NormInputX);
            player.Anim.SetFloat("yVelocity", Mathf.Clamp(Movement.CurrentVelocity.y, -3, 13));
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        }
        
    }
}

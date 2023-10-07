using TGB.Skills;
using TGB.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerAbilityState
{
    private Skill skill;

    private int xInput;
    private bool JumpInput;
    public PlayerSkillState(Unit unit, string animBoolName, Skill skill) : base(unit, animBoolName)
    {

        this.skill = skill;
        this.skill.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;

        skill.InitializeSkill(this, player.Core);
    }
}
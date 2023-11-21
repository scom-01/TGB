using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : UnitState
{
    protected Player player;
    //Input
    protected int xInput;                   //x축 이동 입력값
    protected int yInput;                   //y축 이동 입력값
    protected bool JumpInput;               //점프 입력값
    protected bool JumpInputStop;           //점프 입력 해체 값
    protected bool dashInput;               //Dash 입력값
    protected bool skill1Input;             //Skill1 입력값
    protected bool skill2Input;             //Skill2 입력값
    //Checks
    protected bool isGrounded;              //Grounded 체크
    protected bool isPlatform;              //Platform 체크
    protected bool isTouchingWall;          //벽 체크 
    protected bool isTouchingWallBack;      //후방 벽과 붙어있는지 체크

    public bool input = false;
    public void SetInput(ref bool input) => this.input = input;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Death.isDead)
            return;
    }

    public PlayerState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
        player = unit as Player;
        this.animBoolName = animBoolName;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //Check
        isGrounded = CollisionSenses.CheckIfGrounded || CollisionSenses.CheckIfPlatform;
        isPlatform = CollisionSenses.CheckIfPlatform;
        isTouchingWall = CollisionSenses.CheckIfTouchingWall;
        isTouchingWallBack = CollisionSenses.CheckIfTouchingWallBack;

        //Input
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        JumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;

        skill1Input = player.InputHandler.Skill1Input;
        skill2Input = player.InputHandler.Skill2Input;

        dashInput = player.InputHandler.DashInput;
    }
}

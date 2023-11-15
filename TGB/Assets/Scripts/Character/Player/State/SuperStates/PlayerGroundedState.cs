using System.Collections;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    //Input
    protected int xInput;           //x축 이동 입력값
    protected int yInput;           //y축 이동 입력값
    private bool JumpInput;         //점프 입력값
    private bool dashInput;         //Dash 입력값
    private bool skill1Input;       //Skill1 입력값
    private bool skill2Input;       //Skill2 입력값
    //Checks
    private bool isGrounded;        //Grounded 체크
    private bool isPlatform;        //Platform 체크
    private bool isTouchingWall;    //벽 체크 

    public PlayerGroundedState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = CollisionSenses.CheckIfGrounded || CollisionSenses.CheckIfPlatform;
        isPlatform = CollisionSenses.CheckIfPlatform;
        isTouchingWall = CollisionSenses.CheckIfTouchingWall;
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
    }

    public override void Enter()
    {
        base.Enter();
        unit.isFixedMovement = false;
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //대쉬카운트 초기화
        if (player.DashState.CheckIfResetDash())
        {
            player.DashState.ResetDash(player.playerData.dashCount);
        }

        //Primary 키로 공격 시
        if (player.InputHandler.ActionInputs[(int)CombatInputs.primary])
        {
            player.PrimaryAttackState.SetWeapon(player.Inventory.Weapon);
            if (player.PrimaryAttackState.CheckCommand(isGrounded, ref player.Inventory.Weapon.CommandList))
            {
                player.FSM.ChangeState(player.PrimaryAttackState);
            }
        }
        //Secondary 키로 공격 시
        else if (player.InputHandler.ActionInputs[(int)CombatInputs.secondary])
        {
            player.SecondaryAttackState.SetWeapon(player.Inventory.Weapon);
            if (player.SecondaryAttackState.CheckCommand(isGrounded, ref player.Inventory.Weapon.CommandList))
            {
                player.FSM.ChangeState(player.SecondaryAttackState);
            }
        }
        else if (player.InputHandler.Skill1Input)
        {
            //FSM.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.Skill2Input)
        {
            //FSM.ChangeState(player.SecondaryAttackState);
        }
        //아래로 점프
        else if (JumpInput && isPlatform && yInput < 0)
        {
            player.InputHandler.JumpInput = false;
            player.StartCoroutine(player.DisableCollision());
            return;
        }
        //점프
        else if (JumpInput && player.JumpState.CanJump() && isGrounded && yInput >= 0 && !player.CC2D.isTrigger)
        {
            player.FSM.ChangeState(player.JumpState);
            return;
        }
        //공중에 있을 때 (ex. 절벽에서 걸어서 떨어졌을 때)
        else if (!(isGrounded || CollisionSenses.CheckIfAirGrounded || CollisionSenses.CheckIfPlatformGrounded))
        {
            player.InAirState.StartCoyoteTime();
            player.FSM.ChangeState(player.InAirState);
        }
        //대쉬
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            player.FSM.ChangeState(player.DashState);
        }
    }
}

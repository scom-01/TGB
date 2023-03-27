using System.Collections;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    //Input
    protected int xInput;           //x축 이동 입력값
    protected int yInput;           //y축 이동 입력값
    private bool JumpInput;         //점프 입력값
    private bool dashInput;         //Dash 입력값
    private bool blockInput;        //Block 입력값
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

        isGrounded = CollisionSenses.CheckIfGrounded;
        isPlatform = CollisionSenses.CheckIfPlatform;
        isTouchingWall = CollisionSenses.CheckIfTouchingWall;
    }

    public override void Enter()
    {
        base.Enter();
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
        
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        blockInput = player.InputHandler.BlockInput;
        
        if (player.DashState.CheckIfResetDash())
        {
            player.DashState.ResetDash(player.playerData.dashCount);
        }

        if (player.InputHandler.ActionInputs[(int)CombatInputs.primary])
        {
            player.PrimaryAttackState.SetWeapon(player.Inventory.weapons[(int)CombatInputs.primary]);
            if(player.PrimaryAttackState.CheckCommand(ref player.commandQ))
            {
                player.FSM.ChangeState(player.PrimaryAttackState);
            }
        }
        else if(player.InputHandler.ActionInputs[(int)CombatInputs.secondary])
        {
            player.SecondaryAttackState.SetWeapon(player.Inventory.weapons[(int)CombatInputs.primary]);
            if (player.SecondaryAttackState.CheckCommand(ref player.commandQ))
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
        else if (JumpInput && isPlatform && isGrounded && yInput < 0)
        {
            player.StartCoroutine(player.DisableCollision());
            return;
        }
        else if (JumpInput && player.JumpState.CanJump() && isGrounded && yInput >= 0 && !player.BC2D.isTrigger)
        {
            player.FSM.ChangeState(player.JumpState);
            return;
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            player.FSM.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            player.FSM.ChangeState(player.DashState);
        }
        else if(blockInput)
        {
            player.FSM.ChangeState(player.BlockState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

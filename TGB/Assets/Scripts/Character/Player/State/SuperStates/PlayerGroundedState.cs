public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Unit unit, string animBoolName) : base(unit, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        else if (!(isGrounded))
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

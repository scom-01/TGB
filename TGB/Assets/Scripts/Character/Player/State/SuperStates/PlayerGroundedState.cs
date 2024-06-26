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

        if (CheckActionInput())
        {
            return;
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
            return;
        }
        //대쉬
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            player.FSM.ChangeState(player.DashState);
            return;
        }
    }
}

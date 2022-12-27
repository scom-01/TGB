using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;

    private Vector2 workspace;

    private int xInput;
    private int yInput;

    public PlayerLedgeClimbState(Player player, PlayerFSM fSM, PlayerData playerData, string animBoolName) : base(player, fSM, playerData, animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        player.Core.Movement.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (player.Core.Movement.FancingDirection * playerData.startOffset.x), cornerPos.y - (playerData.startOffset.y));
        stopPos.Set(cornerPos.x + (player.Core.Movement.FancingDirection * playerData.stopOffset.x), cornerPos.y + (playerData.stopOffset.y));

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if(isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            FSM.ChangeState(player.IdleState);
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            jumpInput = player.InputHandler.JumpInput;

            player.Core.Movement.SetVelocityZero();
            player.transform.position = startPos;

            if (xInput == player.Core.Movement.FancingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                FSM.ChangeState(player.InAirState);
            }
            //매달리기 상태에서 벽 점프
            else if(jumpInput && !isClimbing && isHanging)
            {
                player.WallJumpState.DetermineWallJumpDirection(true);
                FSM.ChangeState(player.WallJumpState);
            }
        }
    }
    public Vector2 DetermineCornerPosition()
    {
        //x RayCast를 통한 캐릭터 전방 wallCheck
        RaycastHit2D xHit = Physics2D.Raycast(core.CollisionSenses.GroundCheck.position, Vector2.right * core.Movement.FancingDirection, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * core.Movement.FancingDirection, 0f);
        //y RayCast를 통한 Corner와 ledgeCheck Position과의 차 계산
        RaycastHit2D yHit = Physics2D.Raycast(core.CollisionSenses.LedgeCheck.position + (Vector3)(workspace), Vector2.down, core.CollisionSenses.LedgeCheck.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(core.CollisionSenses.WallCheck.position.x + (xDist * core.Movement.FancingDirection), core.CollisionSenses.LedgeCheck.position.y - yDist);
        return workspace;
    }
    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
}

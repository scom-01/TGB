using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static PlayerInputHandler;

public class Player : Unit
{
    #region State Variables
    
    //PlayerState
    public PlayerInAirState InAirState { get; private set; }

    //PlayerGroundedState
    public PlayerIdleState IdleState { get; private set; }   
    public PlayerMoveState MoveState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    //PlayerAbilityState
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWeaponState BlockState { get; private set; }

    public PlayerWeaponState PrimaryAttackState { get; private set; }
    public PlayerWeaponState SecondaryAttackState { get; private set; }

    //PlayerTouchingWallState
    public PlayerWallSlideState WallSlideState { get; private set; }
    #endregion

    #region Components
    public PlayerInputHandler InputHandler { get; private set; }
    [HideInInspector]
    public PlayerData playerData;
    #endregion



    #region Other Variables            
    private Vector2 workspace;
    //public float invincibleTime;
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();
        playerData = UnitData as PlayerData;
        //각 State 생성
        IdleState = new PlayerIdleState(this, "idle");
        MoveState = new PlayerMoveState(this, "move");
        JumpState = new PlayerJumpState(this, "inAir");    //점프하는 순간 공중상태이므로
        InAirState = new PlayerInAirState(this, "inAir");
        LandState = new PlayerLandState(this, "land");
        WallSlideState = new PlayerWallSlideState(this, "wallSlide");        
        WallJumpState= new PlayerWallJumpState(this, "inAir");
        DashState = new PlayerDashState(this, "dash");
        PrimaryAttackState = new PlayerWeaponState(this, "weapon");
        SecondaryAttackState = new PlayerWeaponState(this, "weapon");        
    }

    protected override void Start()
    {
        base.Start();
        
        InputHandler = GetComponent<PlayerInputHandler>();
        if (InputHandler == null) InputHandler = this.GameObject().AddComponent<PlayerInputHandler>();
        
        FSM.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        //Core.LogicUpdate();
        if (playerData.invincibleTime > 0.0f)
        {
            playerData.invincibleTime -= Time.deltaTime;

            if (playerData.invincibleTime <= 0.0f)
            {
                playerData.invincibleTime = 0f;
            }
        }
        //fsm.CurrentState.LogicUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //fsm.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Func

    public void SetColliderHeight(float height , bool pivot = true)
    {
        if(BC2D == null)
        {
            Debug.Log("BoxCollider is null");
            return;
        }

        //pivot = true -> offset 고정하고 Height 변경, false -> offset 무시하고 Height 변경
        if (pivot)
        {
            Vector2 center = BC2D.offset;
            workspace.Set(BC2D.size.x, height);

            center.y += (height - BC2D.size.y) / 2;

            BC2D.size = workspace;
            BC2D.offset = center;
        }
        else
        {
            Vector2 center = BC2D.offset;
            workspace.Set(BC2D.size.x, height);

            BC2D.size = workspace;
            BC2D.offset = center;
        }        
    }

    #endregion

    #region Other Func

    private void AnimationTrigger() => FSM.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => FSM.CurrentState.AnimationFinishTrigger();

   
    #endregion

    #region Anim Event Func
    public void Attack()
    {
        InputHandler.UseSkill1Input();

        Debug.Log("Attack");
        Collider2D[] targets = Physics2D.OverlapBoxAll(this.gameObject.transform.position + new Vector3(BC2D.size.x, 0, 0) * Core.Movement.FancingDirection, BC2D.size, 0f);

        //범위 내 공격 대상 체크
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.layer >= 16 && targets[i].gameObject.layer <= 18)
                {
                    Debug.Log(targets.ToString());
                }
            }
        }
    }

    public void Hit(float damage)
    {
        //if (SR == null) SR = this.GameObject().AddComponent<SpriteRenderer>();

        Debug.Log("Hit");
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        SR.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        SR.color = Color.white;
    }

    /*public void Push(float power)
    {
        if (!Physics2D.OverlapBox(transform.position + new Vector3((Core.Movement.FancingDirection * power) / 2, BC2D.offset.y, 0),
                                new Vector2(BC2D.bounds.size.x / 2 + power, BC2D.bounds.size.y * 0.95f), 0f, Core.CollisionSenses.WhatIsGround)) 
        {
            this.transform.Translate(new Vector3(power, 0, 0));
        }

        *//*if (!Physics2D.Raycast(groundCheck.position, Vector2.right * FancingDirection, BC2D.size.x / 2 + power, playerData.whatIsGround))
        {
            this.transform.Translate(new Vector3(power, 0, 0));
        }*//*
    }*/
    public void KnockBack(float power)
    {
        if (!Physics2D.OverlapBox(transform.position + new Vector3((-Core.Movement.FancingDirection * power) / 2, BC2D.offset.y, 0),
                                new Vector2(BC2D.bounds.size.x / 2 + power, BC2D.bounds.size.y * 0.95f), 0f, Core.CollisionSenses.WhatIsGround))
        {
            this.transform.Translate(new Vector3(-Core.Movement.FancingDirection * power, 0, 0));
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + new Vector3(BC2D.size.x, 0, 0) * Core.Movement.FancingDirection, BC2D.size);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 23)   //Trap
        {
            if (playerData.invincibleTime == 0f)
            {
                Hit(5);
                playerData.invincibleTime = 1.5f;
            }
        }
    }
}

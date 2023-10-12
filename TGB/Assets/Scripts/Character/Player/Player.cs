using TGB.CoreSystem;
using TGB.Item;
using TGB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
        WallJumpState = new PlayerWallJumpState(this, "inAir");
        DashState = new PlayerDashState(this, "dash");
        PrimaryAttackState = new PlayerWeaponState(this, "action", ((int)CombatInputs.primary == (int)CombatInputs.primary)); //, Inventory.weapon);
        SecondaryAttackState = new PlayerWeaponState(this, "action", ((int)CombatInputs.primary == (int)CombatInputs.secondary));//, Inventory.weapons[(int)CombatInputs.secondary]);        
        Inventory.Weapon.SetCore(Core);
    }

    private void Init()
    {
        InputHandler = GameManager.Inst.GetComponent<PlayerInputHandler>();
        if (InputHandler == null) InputHandler = GameManager.Inst.GameObject().AddComponent<PlayerInputHandler>();

        FSM.Initialize(IdleState);
    }

    protected override void Start()
    {
        base.Start();

        Init();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //fsm.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Func

    private void AnimationTrigger() => FSM.CurrentState.AnimationTrigger();

    #endregion

    #region Override

    public override void DieEffect()
    {
        base.DieEffect();
        GameManager.Inst.Pause();
        GameManager.Inst.ChangeUI(UI_State.Result);
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
        GameManager.Inst.ResetData();
        GameManager.Inst.ResultUI.resultPanel.UpdateResultPanel();
    }

    public override void HitEffect()
    {
        base.HitEffect();

    }
    #endregion
}
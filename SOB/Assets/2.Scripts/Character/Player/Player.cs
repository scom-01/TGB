using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerFSM fsm { get; private set; }
    
    public PlayerIdleState IdleState { get; private set; }   
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FancingDirection { get; private set; }
    
    private Vector2 workspace;
    #endregion

    #region Unity Callback Func
    private void Awake()
    {
        fsm = new PlayerFSM();

        IdleState = new PlayerIdleState(this, fsm, playerData, "idle");
        MoveState = new PlayerMoveState(this, fsm, playerData, "move");
        JumpState = new PlayerJumpState(this, fsm, playerData, "inAir");    //점프하는 순간 공중상태이므로
        InAirState = new PlayerInAirState(this, fsm, playerData, "inAir");
        LandState = new PlayerLandState(this, fsm, playerData, "land");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        FancingDirection = 1;

        fsm.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        fsm.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        fsm.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Func
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void  SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Func

    public bool CheckIfGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position,playerData.groundCheckRadius,playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FancingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Func

    private void AnimationTrigger() => fsm.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => fsm.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FancingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}

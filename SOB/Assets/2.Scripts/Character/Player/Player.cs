using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerFSM fsm { get; private set; }
    
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
    public PlayerBlockState BlockState { get; private set; }
    public PlayerAttackState AttackState {get; private set; }

    //PlayerTouchingWallState
    public PlayerWallSlideState WallSlideState { get; private set; }

    //public PlayerWallGrabState WallGrabState { get; private set; }
    //public PlayerWallClimbState WallClimbState { get; private set; }
    //public PlayerLedgeClimbState LedgeClimbState { get; private set; }


    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC2D { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
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

        //각 State 생성
        IdleState = new PlayerIdleState(this, fsm, playerData, "idle");
        MoveState = new PlayerMoveState(this, fsm, playerData, "move");
        JumpState = new PlayerJumpState(this, fsm, playerData, "inAir");    //점프하는 순간 공중상태이므로
        InAirState = new PlayerInAirState(this, fsm, playerData, "inAir");
        LandState = new PlayerLandState(this, fsm, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, fsm, playerData, "wallSlide");
        //WallGrabState = new PlayerWallGrabState(this, fsm, playerData, "wallGrab");
        //WallClimbState = new PlayerWallClimbState(this, fsm, playerData, "wallClimb");
        WallJumpState= new PlayerWallJumpState(this, fsm, playerData, "inAir");
        //LedgeClimbState = new PlayerLedgeClimbState(this, fsm, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, fsm, playerData, "dash");
        BlockState = new PlayerBlockState(this, fsm, playerData, "block");
        AttackState = new PlayerAttackState(this, fsm, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        BC2D = GetComponent<BoxCollider2D>();

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

            //center.y += (height - BC2D.size.y) / 2;

            BC2D.size = workspace;
            BC2D.offset = center;
        }        
    }

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, Mathf.Clamp(angle.y * velocity,-3,13));
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
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
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(BC2D.bounds.size.x*0.95f, BC2D.bounds.size.y * playerData.groundCheckRadius), 0f, playerData.whatIsGround);
        //return Physics2D.OverlapCircle(groundCheck.position,playerData.groundCheckRadius,playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        Debug.DrawRay(wallCheck.position, Vector2.right * FancingDirection * playerData.wallCheckDistance, Color.green);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FancingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FancingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        Debug.DrawRay(wallCheck.position, Vector2.right * -FancingDirection * playerData.wallCheckDistance, Color.red);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FancingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
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

    //Ledge를 위한 Corner Check 및 Position 계산
    public Vector2 DetermineCornerPosition()
    {
        //x RayCast를 통한 캐릭터 전방 wallCheck
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FancingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * FancingDirection, 0f);
        //y RayCast를 통한 Corner와 ledgeCheck Position과의 차 계산
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FancingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }
    private void AnimationTrigger() => fsm.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => fsm.CurrentState.AnimationFinishTrigger();

    //2D Filp
    private void Flip()
    {
        FancingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void Attack()
    {
        InputHandler.UseSkill1Input();

        Collider2D[] targets = Physics2D.OverlapBoxAll(this.gameObject.transform.position + new Vector3(BC2D.size.x, 0, 0) * FancingDirection, BC2D.size, 0f);
        
        //범위 내 공격 대상 체크
        if (targets.Length > 0)
        {
            for(int i = 0; i < targets.Length; i++)
            {
                if(targets[i].gameObject.layer >=16 && targets[i].gameObject.layer <=18)
                {
                    Debug.Log(targets.ToString());
                }
            }            
        }
    }

    public void ComoboCheck()
    {
        if(!InputHandler.Skill1Input)
        {
            AttackState.ComboCheck();
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(BC2D.size.x, 0, 0) * FancingDirection, BC2D.size);
    }
}

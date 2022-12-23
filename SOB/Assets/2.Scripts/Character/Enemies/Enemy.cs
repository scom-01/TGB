using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerInputHandler;

public class Enemy : MonoBehaviour
{
    #region State Variables
    public EnemyFSM fsm { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHitState HitState { get; private set; }



    [SerializeField]
    private EnemyData enemyData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC2D { get; private set; }

    public SpriteRenderer SR { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FancingDirection { get; private set; }        
    #endregion

    #region Unity Callback Func
    private void Awake()
    {
        IdleState = new EnemyIdleState(this, fsm, enemyData, "idle");
        RunState = new EnemyRunState(this, fsm, enemyData, "run");
        AttackState = new EnemyAttackState(this, fsm, enemyData, "attack");
        HitState = new EnemyHitState(this, fsm, enemyData, "hit");
    }

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        if (Anim == null) Anim = this.GameObject().AddComponent<Animator>();

        RB = GetComponent<Rigidbody2D>();
        if (RB == null) RB = this.GameObject().AddComponent<Rigidbody2D>();

        BC2D = GetComponent<BoxCollider2D>();
        if (BC2D == null) BC2D = this.GameObject().AddComponent<BoxCollider2D>();

        SR = GetComponent<SpriteRenderer>();
        if (SR == null) SR = this.GameObject().AddComponent<SpriteRenderer>();


        //PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        FancingDirection = 1;

        fsm.Initialize(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        CurrentVelocity = RB.velocity;
        if (enemyData.invincibleTime > 0.0f)
        {
            enemyData.invincibleTime -= Time.deltaTime;

            if (enemyData.invincibleTime <= 0.0f)
            {
                enemyData.invincibleTime = 0f;
            }
        }
        fsm.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        fsm.CurrentState.PhysicsUpdate();
    }
    #endregion
    #region Check Func

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(BC2D.bounds.size.x * 0.95f, BC2D.bounds.size.y * enemyData.groundCheckRadius), 0f, enemyData.whatIsGround);
        //return Physics2D.OverlapCircle(groundCheck.position,enemyData.groundCheckRadius,enemyData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        Debug.DrawRay(wallCheck.position, Vector2.right * FancingDirection * enemyData.wallCheckDistance, Color.green);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FancingDirection, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        Debug.DrawRay(wallCheck.position, Vector2.right * -FancingDirection * enemyData.wallCheckDistance, Color.red);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FancingDirection, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }
    #endregion

    public void Flip()
    {
        FancingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}

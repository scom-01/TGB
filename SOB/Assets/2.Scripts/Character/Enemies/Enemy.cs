using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Variables
    public EnemyFSM fsm { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHitState HitState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }



    [SerializeField]
    private EnemyData enemyData;
    #endregion

    #region Components

    public EnemyCore enemyCore { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC2D { get; private set; }

    public SpriteRenderer SR { get; private set; }
    #endregion

    #region Other Variables

    private float currentHealth;
    public float damageDirection { get; private set; }
    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }
    #endregion

    #region Unity Callback Func
    private void Awake()
    {
        enemyCore = GetComponentInChildren<EnemyCore>();

        fsm = new EnemyFSM();

        IdleState = new EnemyIdleState(this, fsm, enemyData, "idle");
        RunState = new EnemyRunState(this, fsm, enemyData, "run");
        AttackState = new EnemyAttackState(this, fsm, enemyData, "attack");
        HitState = new EnemyHitState(this, fsm, enemyData, "hit");
        DeadState = new EnemyDeadState(this, fsm, enemyData, "dead");
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

        fsm.Initialize(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        enemyCore.LogicUpdate();
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

    #region Other Func
    public void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1]>this.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit Particle

        if (currentHealth > 0.0f)
        {
            fsm.ChangeState(HitState);
        }
        else if (currentHealth <= 0.0f) 
        {
            fsm.ChangeState(DeadState);
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(enemyCore.CollisionSenses.GroundCheck.position, enemyCore.CollisionSenses.GroundCheck.position + Vector3.down * enemyCore.CollisionSenses.GroundCheckRadius);
        Gizmos.DrawLine(enemyCore.CollisionSenses.WallCheck.position, enemyCore.CollisionSenses.WallCheck.position + Vector3.right * enemyCore.Movement.FancingDirection * enemyCore.CollisionSenses.WallCheckDistance);
        Gizmos.DrawLine(enemyCore.CollisionSenses.WallCheck.position, enemyCore.CollisionSenses.WallCheck.position+ Vector3.right * -enemyCore.Movement.FancingDirection * enemyCore.CollisionSenses.WallCheckDistance);
        Gizmos.DrawLine(enemyCore.CollisionSenses.transform.position, enemyCore.CollisionSenses.transform.position + Vector3.right * 1.1f * enemyCore.Movement.FancingDirection);
        Gizmos.DrawCube(transform.position + new Vector3((BC2D.offset.x + 0.1f) * enemyCore.Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x, BC2D.bounds.size.y * 0.95f)); // enemyCore.CollisionSenses.GroundCheck.position + new Vector3(BC2D.offset.x + BC2D.size.x / 2, 0, 0) * enemyCore.Movement.FancingDirection + Vector3.down);
        Gizmos.DrawLine(enemyCore.CollisionSenses.transform.position, enemyCore.CollisionSenses.transform.position + Vector3.right * enemyCore.Movement.FancingDirection * enemyCore.GetComponentInParent<Enemy>().EnemyData.playerDetectedDistance);
    }
}

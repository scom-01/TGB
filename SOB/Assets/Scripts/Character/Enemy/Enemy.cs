using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : Unit
{
    #region State Variables

    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHitState HitState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }


    [HideInInspector]
    public EnemyCore core;
    [HideInInspector]
    public EnemyData enemyData;
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();
        core = Core as EnemyCore;
        enemyData = UnitData as EnemyData;


        IdleState = new EnemyIdleState(this, "idle");
        RunState = new EnemyRunState(this, "run");
        AttackState = new EnemyAttackState(this, "attack");
        HitState = new EnemyHitState(this, "hit");
        DeadState = new EnemyDeadState(this, "dead");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        FSM.Initialize(IdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Anim.SetFloat("yVelocity", enemyCore.Movement.RB.velocity.y);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(Core.CollisionSenses.GroundCheck.position, Core.CollisionSenses.GroundCheck.position + Vector3.down * Core.CollisionSenses.GroundCheckRadius);
        //Gizmos.DrawLine(Core.CollisionSenses.WallCheck.position, Core.CollisionSenses.WallCheck.position + Vector3.right * Core.Movement.FancingDirection * Core.CollisionSenses.WallCheckDistance);
        //Gizmos.DrawLine(Core.CollisionSenses.WallCheck.position, Core.CollisionSenses.WallCheck.position+ Vector3.right * -Core.Movement.FancingDirection * Core.CollisionSenses.WallCheckDistance);
        //Gizmos.DrawLine(Core.CollisionSenses.transform.position, Core.CollisionSenses.transform.position + Vector3.right * 1.1f * Core.Movement.FancingDirection);
        //Gizmos.DrawCube(transform.position + new Vector3((BC2D.offset.x + 0.1f) * Core.Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x, BC2D.bounds.size.y * 0.95f)); // enemyCore.CollisionSenses.GroundCheck.position + new Vector3(BC2D.offset.x + BC2D.size.x / 2, 0, 0) * enemyCore.Movement.FancingDirection + Vector3.down);
        //Gizmos.DrawLine(Core.CollisionSenses.transform.position, Core.CollisionSenses.transform.position + Vector3.right * Core.Movement.FancingDirection * enemyData.playerDetectedDistance);
    }
}

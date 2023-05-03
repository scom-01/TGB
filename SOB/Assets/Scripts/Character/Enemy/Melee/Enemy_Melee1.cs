using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1 : Enemy
{
    #region State Variables
    public Enemy_Melee1_AttackState AttackState { get; private set; }
    public Enemy_Melee1_IdleState IdleState { get; private set; }
    public Enemy_Melee1_MoveState RunState { get; private set; }
    public Enemy_Melee1_HitState HitState { get; private set; }
    public Enemy_Melee1_DeathState DeathState { get; private set; }
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();

        AttackState = new Enemy_Melee1_AttackState(this, "action");
        IdleState = new Enemy_Melee1_IdleState(this, "idle");
        RunState = new Enemy_Melee1_MoveState(this, "run");
        HitState = new Enemy_Melee1_HitState(this, "hit");
        DeathState = new Enemy_Melee1_DeathState(this, "death");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    public override void HitEffect()
    {
        base.HitEffect();
        FSM.ChangeState(HitState);
    }

    public override void DieEffect()
    {
        base.DieEffect();
        FSM.ChangeState(DeathState);
    }

    protected override void Init()
    {
        base.Init();
        FSM.Initialize(IdleState);
    }
}


using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer1 : Unit
{
    #region State Variables

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
        //Anim.SetFloat("yVelocity", enemyCore.Movement.RB.velocity.y);
        if (core.GetCoreComponent<UnitStats>().invincibleTime > 0.0f)
        {
            core.GetCoreComponent<UnitStats>().invincibleTime -= Time.deltaTime;

            if (core.GetCoreComponent<UnitStats>().invincibleTime <= 0.0f)
            {
                core.GetCoreComponent<UnitStats>().invincibleTime = 0f;
            }
        }
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


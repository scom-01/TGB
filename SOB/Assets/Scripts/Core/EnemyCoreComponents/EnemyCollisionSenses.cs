using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{
    public bool isUnitInAttackArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(groundCheck.position, Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }

    public bool isUnitInDetectedArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(groundCheck.position, Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }

    public GameObject UnitDetectArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            if (ray1.collider != null) 
            {
                return ray1.collider.gameObject;
            }
            return null;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(transform.position + new Vector3((BC2D.offset.x+ (core.Unit.UnitData as EnemyData).UnitDetectedDistance/2) * Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x/2+ (core.Unit.UnitData as EnemyData).UnitDetectedDistance, BC2D.bounds.size.y * 0.95f));
        if (BC2D == null)
            return;
        if (CC2D == null)
            return;

        Gizmos.DrawWireCube(transform.position + new Vector3((BC2D.offset.x) * Movement.FancingDirection, (BC2D.offset.y - (CC2D.radius / 2))), new Vector2(BC2D.bounds.size.x, (BC2D.bounds.size.y + CC2D.radius) * 0.95f));

        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);        
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);

        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitAttackDistance, Color.red);        
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitAttackDistance, Color.red);
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitAttackDistance, Color.red);

        //CheckIfCliff
        Debug.DrawRay(groundCheck.position + new Vector3((BC2D.offset.x + 1) + BC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down * 0.5f, Color.blue);

        //CheckIfTouchingWallBack
        Debug.DrawRay(wallCheck.position, Vector2.right * -Movement.FancingDirection * (WallCheckDistance+ BC2D.bounds.size.x/2), Color.red);

        //CheckIfTouchingWall
        Debug.DrawRay(wallCheck.position, Vector2.right * Movement.FancingDirection * (WallCheckDistance + BC2D.bounds.size.x/2), Color.green);
    }
}

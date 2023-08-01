using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{
    public bool isUnitInAttackArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(groundCheck.position, Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }

    public bool isUnitInFrontDetectedArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(groundCheck.position, Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }
    public bool isUnitInBackDetectedArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y * 0.5f), Vector2.right * -Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y), Vector2.right * -Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(groundCheck.position, Vector2.right * -Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }

    public GameObject UnitFrontDetectArea
    {
        get
        {
            Vector2 offset = Vector2.zero;
            Vector2 size = new Vector2(CC2D.size.x + (core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y);
            offset.Set(groundCheck.position.x + (-CC2D.size.x * Movement.FancingDirection), groundCheck.position.y);
            var detected = Physics2D.OverlapBoxAll(offset, size, 0f, core.Unit.UnitData.WhatIsEnemyUnit);

            foreach (Collider2D coll in detected)
            {
                if (coll.tag == this.tag)
                    continue;

                if (coll.GetComponent<Unit>())
                {
                    return coll.gameObject;
                }
            }
            //    RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y + BC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            //if (ray1.collider != null) 
            //{
            //    return ray1.collider.gameObject;
            //}
            return null;
        }
    }
    public GameObject UnitBackDetectArea
    {
        get
        {
            Vector2 offset = Vector2.zero;
            Vector2 size = new Vector2(CC2D.size.x + (core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y);
            offset.Set(groundCheck.position.x + (-CC2D.size.x * Movement.FancingDirection), groundCheck.position.y);
            var detected = Physics2D.OverlapBoxAll(offset, size, 0f, core.Unit.UnitData.WhatIsEnemyUnit);

            foreach (Collider2D coll in detected)
            {
                if (coll.GetComponent<Unit>())
                {
                    return coll.gameObject;
                }
            }
            return null;
        }
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        if (CC2D == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(groundCheck.position.x + (CC2D.size.x/2 * Movement.FancingDirection), groundCheck.position.y+CC2D.size.y*0.5f, 0), new Vector2(CC2D.size.x + (core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y)); ;

        //Gizmos.DrawWireCube(transform.position + new Vector3((CC2D.offset.x) * Movement.FancingDirection, (CC2D.offset.y - (CAPC2D.radius / 2))), new Vector2(CC2D.bounds.size.x, (CC2D.bounds.size.y + CAPC2D.radius) * 0.95f));

        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);        
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);

        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitAttackDistance, Color.red);        
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitAttackDistance, Color.red);
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y), Vector2.right * Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitAttackDistance, Color.red);

        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y * 0.5f), Vector2.right *  -Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y + CC2D.bounds.size.y), Vector2.right * -Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);
        Debug.DrawRay(new Vector2(groundCheck.position.x, groundCheck.position.y), Vector2.right * -Movement.FancingDirection * (core.Unit.UnitData as EnemyData).UnitDetectedDistance, Color.cyan);

        //CheckIfCliff
        Debug.DrawRay(groundCheck.position + new Vector3((CC2D.offset.x + 1) + CC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down * 0.5f, Color.blue);

        //CheckIfTouchingWallBack
        Debug.DrawRay(wallCheck.position, Vector2.right * -Movement.FancingDirection * (WallCheckDistance+ CC2D.bounds.size.x/2), Color.red);

        //CheckIfTouchingWall
        Debug.DrawRay(wallCheck.position, Vector2.right * Movement.FancingDirection * (WallCheckDistance + CC2D.bounds.size.x/2), Color.green);
    }

    protected override void Awake()
    {
        base.Awake();
        this.tag = core.Unit.gameObject.tag;
    }
}

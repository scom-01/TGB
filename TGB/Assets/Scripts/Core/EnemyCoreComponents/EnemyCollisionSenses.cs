using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{
    public bool isUnitInAttackArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(GroundCenterPos.x, GroundCenterPos.y + CC2D.bounds.size.y * 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(GroundCenterPos.x, GroundCenterPos.y + CC2D.bounds.size.y), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(GroundCenterPos, Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }

    public bool isUnitInFrontDetectedArea
    {
        get
        {
            var RayHit = Physics2D.BoxCastAll
                (
                    new Vector2(GroundCenterPos.x, GroundCenterPos.y + CC2D.bounds.size.y * 0.5f),
                    CC2D.bounds.size,
                    0f,
                    Vector2.right * Movement.FancingDirection,
                    (core.Unit.UnitData as EnemyData).UnitDetectedDistance,
                    core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit
                );
            return (RayHit.Length > 0);
        }
    }
    public bool isUnitInBackDetectedArea
    {
        get
        {
            var RayHit = Physics2D.BoxCastAll
                (
                    new Vector2(GroundCenterPos.x, GroundCenterPos.y + CC2D.bounds.size.y * 0.5f),
                    CC2D.bounds.size,
                    0f,
                    Vector2.right * -Movement.FancingDirection,
                    (core.Unit.UnitData as EnemyData).UnitDetectedDistance,
                    core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit
                );
            return (RayHit.Length > 0);
        }
    }
    public bool isUnitDetectedCircle
    {
        get
        {
            var RayHit = Physics2D.CircleCastAll
                (
                    new Vector2(GroundCenterPos.x, GroundCenterPos.y + CC2D.bounds.size.y * 0.5f),
                    (core.Unit.UnitData as EnemyData).UnitDetectedDistance,
                    Vector2.right * Movement.FancingDirection,
                    0,
                    core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit
                );
            return (RayHit.Length > 0);
        }
    }
    public GameObject UnitFrontDetectArea
    {
        get
        {
            var RayHit = Physics2D.BoxCastAll
                (
                    new Vector2(GroundCenterPos.x, GroundCenterPos.y + CC2D.bounds.size.y * 0.5f),
                    CC2D.bounds.size,
                    0f,
                    Vector2.right * Movement.FancingDirection,
                    (core.Unit.UnitData as EnemyData).UnitDetectedDistance,
                    core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit
                );
            foreach (var coll in RayHit)
            {
                if (coll.collider.GetComponent<Unit>())
                {
                    return coll.collider.gameObject;
                }
            }
            return null;

            //Vector2 offset = Vector2.zero;
            //Vector2 size = new Vector2(CC2D.size.x + (core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y);
            //offset.Set(GroundCenterPos.x + (CC2D.size.x * Movement.FancingDirection), GroundCenterPos.y);
            //var detected = Physics2D.OverlapBoxAll(offset, size, 0f, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);

            //foreach (Collider2D coll in detected)
            //{
            //    if (coll.GetComponent<Unit>())
            //    {
            //        return coll.gameObject;
            //    }
            //}
            //return null;
            //var RayHit = Physics2D.BoxCastAll
            //    (
            //        new Vector2(GroundCenterPos.x + (CC2D.size.x / 2 * Movement.FancingDirection), GroundCenterPos.y + CC2D.bounds.size.y * 0.5f),
            //        CC2D.bounds.size,
            //        0f,
            //        Vector2.right * Movement.FancingDirection,
            //        Mathf.Abs((core.Unit.UnitData as EnemyData).UnitDetectedDistance - CC2D.bounds.size.x),
            //        core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit
            //    );
            //if (RayHit.Length > 0)
            //{
            //    foreach (var obj in RayHit)
            //    {
            //        if (obj.transform.tag == this.tag)
            //        {
            //            continue;
            //        }

            //        if (obj.transform.gameObject.GetComponent<Unit>())
            //        {
            //            return obj.transform.gameObject;
            //        }
            //    }
            //}
            //return null;
        }
    }
    public GameObject UnitBackDetectArea
    {
        get
        {
            Vector2 offset = Vector2.zero;
            Vector2 size = new Vector2(CC2D.size.x + (core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y);
            offset.Set(GroundCenterPos.x + (-CC2D.size.x * Movement.FancingDirection), GroundCenterPos.y);
            var detected = Physics2D.OverlapBoxAll(offset, size, 0f, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);

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
    public GameObject UnitDetectedCircle
    {
        get
        {
            Vector2 offset = Vector2.zero;
            float size = (core.Unit.UnitData as EnemyData).UnitDetectedDistance;
            offset.Set(GroundCenterPos.x + (-CC2D.size.x * Movement.FancingDirection), GroundCenterPos.y);
            var detected = Physics2D.OverlapCircleAll(offset, size, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);

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
        //AttackArea
        Gizmos.DrawWireCube(
            new Vector3(GroundCenterPos.x + ((CC2D.size.x / 2 + (core.Unit.UnitData as EnemyData).UnitAttackDistance / 2) * Movement.FancingDirection), GroundCenterPos.y + CC2D.size.y * 0.5f, 0),
            new Vector2((core.Unit.UnitData as EnemyData).UnitAttackDistance, CC2D.bounds.size.y));
        Gizmos.DrawWireCube(
            new Vector3(GroundCenterPos.x + ((CC2D.size.x / 2 + (core.Unit.UnitData as EnemyData).UnitAttackDistance / 2) * -1f * Movement.FancingDirection), GroundCenterPos.y + CC2D.size.y * 0.5f, 0),
            new Vector2((core.Unit.UnitData as EnemyData).UnitAttackDistance, CC2D.bounds.size.y));

        Gizmos.color = Color.red;
        //front
        Gizmos.DrawWireCube(
            new Vector3(GroundCenterPos.x + ((CC2D.size.x / 2 + (core.Unit.UnitData as EnemyData).UnitDetectedDistance / 2) * Movement.FancingDirection), GroundCenterPos.y + CC2D.size.y * 0.5f, 0),
            new Vector2((core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y));

        Gizmos.color = Color.blue;
        //back
        Gizmos.DrawWireCube(
            new Vector3(GroundCenterPos.x + ((CC2D.size.x / 2 + (core.Unit.UnitData as EnemyData).UnitDetectedDistance / 2) * -1f * Movement.FancingDirection), GroundCenterPos.y + CC2D.size.y * 0.5f, 0),
            new Vector2((core.Unit.UnitData as EnemyData).UnitDetectedDistance, CC2D.bounds.size.y));

        //CheckIfCliff
        Debug.DrawRay(GroundCenterPos + new Vector3((CC2D.offset.x + 1) + CC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down * 0.5f, Color.blue);

        //CheckIfTouchingWallBack
        Debug.DrawRay(WallCheck, Vector2.right * -Movement.FancingDirection * (WallCheckDistance + CC2D.bounds.size.x / 2), Color.red);

        //CheckIfTouchingWall
        Debug.DrawRay(WallCheck, Vector2.right * Movement.FancingDirection * (WallCheckDistance + CC2D.bounds.size.x / 2), Color.green);
    }

    protected override void Awake()
    {
        base.Awake();
        this.tag = core.Unit.gameObject.tag;
    }
}

using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{
    public bool UnitInAttackArea
    {
        get
        {
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right * Movement.FancingDirection, (core.Unit.UnitData as EnemyData).UnitAttackDistance, core.Unit.UnitData.WhatIsEnemyUnit);
            return (ray1 || ray2 || ray3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;

public class EnemyCollisionSenses : CollisionSenses
{
    private Enemy enemy;
    private EnemyMovement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private EnemyMovement movement;

    #region Check Transforms
    public LayerMask WhatIsPlayer { get => enemy.enemyData.whatIsPlayer; set => enemy.enemyData.whatIsPlayer = value; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        enemy = core.Unit as Enemy;
    }

    public bool CheckIfTouching
    {
        get => Physics2D.OverlapBox(transform.position + new Vector3((BC2D.offset.x + 0.1f) * Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x, BC2D.bounds.size.y * 0.95f), 0f, WhatIsGround);
    }

    public bool CheckIfCliff
    {
        get => Physics2D.Raycast(groundCheck.position + new Vector3(BC2D.offset.x + BC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down, 0.2f, WhatIsGround);
    }

    public bool PlayerDectected
    {
        get => Physics2D.Raycast(transform.position, Vector2.right * Movement.FancingDirection, enemy.enemyData.playerDetectedDistance, WhatIsPlayer);
    }
}

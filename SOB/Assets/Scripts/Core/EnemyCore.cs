using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;
public class EnemyCore : Core
{
    public Enemy Enemy { get; private set; }
    public EnemyMovement enemyMovement { get; private set; }
    public EnemyCollisionSenses enemyCollisionSenses { get; private set; }
    public override void Awake()
    {
        base.Awake();
        /*enemyMovement = Movement as EnemyMovement;
        enemyCollisionSenses = CollisionSenses as EnemyCollisionSenses;
        Enemy = Unit as Enemy;

        if (!Movement)
        {
            Debug.LogError("Missing EnemyMovement Core Componenet");
        }

        if (!CollisionSenses)
        {
            Debug.LogError("Missing EnemyCollisionSenses Core Componenet");
        }*/
    }
}

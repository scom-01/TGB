using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public EnemyMovement Movement { get; private set; }
    public EnemyCollisionSenses CollisionSenses { get; private set; }
    private void Awake()
    {
        Movement = GetComponentInChildren<EnemyMovement>();
        CollisionSenses = GetComponentInChildren<EnemyCollisionSenses>();

        if (!Movement)
        {
            Debug.LogError("Missing EnemyMovement Core Componenet");
        }

        if (!CollisionSenses)
        {
            Debug.LogError("Missing EnemyCollisionSenses Core Componenet");
        }
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}

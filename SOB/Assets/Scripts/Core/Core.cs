using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public Player Player { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Player = GetComponentInParent<Player>();

        if (!Movement)
        {
            Debug.LogError("Missing Movement Core Componenet");
        }
        
        if(!CollisionSenses)
        {
            Debug.LogError("Missing CollisionSenses Core Componenet");
        }
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        CollisionSenses.LogicUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public Unit Unit { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }
    public virtual void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Unit = GetComponentInParent<Unit>();

        if (!Movement)
        {
            Debug.LogError("Missing Movement Core Componenet");
        }
        
        if(!CollisionSenses)
        {
            Debug.LogError("Missing CollisionSenses Core Componenet");
        }
    }

    public virtual void LogicUpdate()
    {
        Movement.LogicUpdate();
        CollisionSenses.LogicUpdate();
    }
}

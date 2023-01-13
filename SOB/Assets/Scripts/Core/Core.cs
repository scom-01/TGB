using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement 
    { 
        get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
        private set => movement = value;
    }
    public Unit Unit
    {
        get => GenericNotImplementedError<Unit>.TryGet(unit, transform.parent.name);
        private set => unit = value;
    }
    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
        private set => collisionSenses = value;
    }
    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
        private set => combat = value;
    }

    private Movement movement;
    private Unit unit;
    private CollisionSenses collisionSenses;
    private Combat combat;
    public virtual void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();
        unit = GetComponentInParent<Unit>();
        combat = GetComponentInParent<Combat>();
    }

    public virtual void LogicUpdate()
    {
        movement.LogicUpdate();
        collisionSenses.LogicUpdate();
    }
}

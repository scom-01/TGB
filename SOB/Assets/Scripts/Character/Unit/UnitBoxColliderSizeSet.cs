using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBoxColliderSizeSet : MonoBehaviour
{
    private BoxCollider2D BC2D;
    private Unit Unit;

    void Start()
    {
        Unit = GetComponentInParent<Unit>();
        BC2D = GetComponent<BoxCollider2D>();
        BC2D.isTrigger = true;
        BC2D.offset = Unit.BC2D.offset + new Vector2(0, -(Unit.CC2D.radius / 2));
        BC2D.size = Unit.BC2D.size + new Vector2(0, Unit.CC2D.radius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBoxColliderSizeSet : MonoBehaviour
{
    private BoxCollider2D BC2D;
    private GameObject BC2D1;
        
    void Start()
    {
        BC2D1 = GetComponentInParent<Unit>().gameObject;
        BC2D = GetComponent<BoxCollider2D>();
        BC2D.isTrigger = true;
        BC2D.offset = BC2D1.GetComponent<BoxCollider2D>().offset;
        BC2D.size = BC2D1.GetComponent<BoxCollider2D>().size;
    }
}
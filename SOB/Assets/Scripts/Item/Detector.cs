using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Item;
using UnityEditor.UIElements;

public class Detector : MonoBehaviour
{
    private Unit unit;
    private BoxCollider2D BC2D;
    
    [field: SerializeField]
    [field: Tooltip("설정한 LayerMask만 탐지가능")]
    public LayerMask DetectorMask { get; private set; }
    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
        BC2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {        
        if (unit != null)
        {
            BC2D.size = unit.BC2D.size;
        }
    }

    public void AddLayerMask(string layerName)
    {
        DetectorMask |= (1 << LayerMask.NameToLayer(layerName));
    }

    public void RemoveLayerMask(string layerName)
    {
        DetectorMask = DetectorMask & ~(1 << LayerMask.NameToLayer(layerName));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.transform.gameObject.layer)) < 0)
            return;

        //Trap
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            if (unit.UnitData.invincibleTime == 0f)
            {
                //unit.Hit(5);
                unit.UnitData.invincibleTime = 1.5f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.transform.gameObject.layer)) < 0)
            return;
        
        //Item
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log($"Detected {collision.name} {this.name}");
            var collItem = collision.GetComponent<SOB_Item>();
            collItem.Detected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.transform.gameObject.layer)) < 0)
            return;


        //Item
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log($"UnDetected {collision.name}");
            var collItem = collision.GetComponent<SOB_Item>();
            collItem.UnDetected();
        }
    }
}

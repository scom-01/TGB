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

    private List<GameObject> DetectedList = new List<GameObject>();
    private GameObject currentGO;
    private float currentDistance = 0.0f;
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
            BC2D.offset = unit.BC2D.offset;
        }
    }

    private void Update()
    {

    }
    IEnumerator CheckCurrentGO()
    {
        if (DetectedList.Count > 0)
        {
            foreach (GameObject go in DetectedList)
            {
                if (currentGO == null)
                {
                    currentGO = go;

                    currentGO.GetComponentInParent<SOB_Item>().unit = unit;
                    currentGO.GetComponentInParent<SOB_Item>().Detected(this.gameObject.transform.position.x < currentGO.transform.position.x);
                    continue;
                }

                if (go == null)
                    continue;

                if (Vector2.Distance(currentGO.transform.position, this.gameObject.transform.position) > Vector2.Distance(go.transform.position, this.gameObject.transform.position))
                {
                    //가장 가까운 Detected 오브젝트
                    currentGO = go;
                    Debug.Log($"제일 가까운 오브젝트 {currentGO.name}");
                    currentGO.GetComponentInParent<SOB_Item>().unit = unit;
                    currentGO.GetComponentInParent<SOB_Item>().Detected(this.gameObject.transform.position.x < currentGO.transform.position.x);

                    continue;
                }

            }
        }
        yield return 0;
    }

    public void AddLayerMask(string layerName)
    {
        DetectorMask |= (1 << LayerMask.NameToLayer(layerName));
    }

    public void RemoveLayerMask(string layerName)
    {
        DetectorMask = DetectorMask & ~(1 << LayerMask.NameToLayer(layerName));
    }

    //Detected
    private void OnTriggerStay2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;
        var item = collision.GetComponentInParent<SOB_Item>();
        //EquipmentItem
        if (item.itemData.isEquipment)
        {
            if (!DetectedList.Contains(collision.gameObject))
            {
                DetectedList.Add(collision.gameObject);
            }
            StartCoroutine(CheckCurrentGO());
        }
        //ConsumptionItem
        else
        {
            item.unit = unit;
            item.CallCoroutine(ItemGetType.Collision.ToString());
        }

        //Trap
        if (collision.gameObject.tag == "Trap")
        {
            if (unit.UnitData.invincibleTime == 0f)
            {
                //unit.Hit(5);
                unit.UnitData.invincibleTime = 1.5f;
            }
        }
    }
    //Detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;
        var item = collision.GetComponentInParent<SOB_Item>();
        //Item
        if (item.itemData.isEquipment)
        {
            //Debug.Log($"Detected {collision.name} {this.name}");
            //var collItem = collision.GetComponentInParent<SOB_Item>();
            //collItem.Detected();
        }
    }
    //Detected
    private void OnTriggerExit2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;

        DetectedList.Remove(collision.gameObject);

        var item = collision.GetComponentInParent<SOB_Item>();
        if (item == null)
            return;
        //Item
        if (item.itemData.isEquipment)
        {
            Debug.Log($"UnDetected {this.name}");
            if (currentGO == collision.gameObject)
            {
                item.UnDetected();
                currentGO = null;
            }
        }
    }


    //Collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ////Detected이면 return
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Invisible"))
        //    return;

        ////DetectorMask 의 LayerMask가 아니면 return
        //if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
        //    return;

        //if (collision.gameObject.tag == "Item")
        //{
        //    Debug.Log($"Conflict {collision.gameObject.name}");
        //    var collItem = collision.gameObject.GetComponent<SOB_Item>();
        //    collItem.unit = unit;
        //    collItem.CallCoroutine(ItemGetType.Collision.ToString());
        //}
    }
}

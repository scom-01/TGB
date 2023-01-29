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
                if(currentGO == null)
                {
                    currentGO = go;
                    Debug.Log($"제일 가까운 오브젝트 {currentGO.name}");
                    continue;
                }

                if (go == null)
                    continue;

                if (Vector2.Distance(currentGO.transform.position,this.gameObject.transform.position) > Vector2.Distance(go.transform.position, this.gameObject.transform.position))
                {
                    currentGO = go;
                    Debug.Log($"제일 가까운 오브젝트 {currentGO.name}");
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;

        if (!DetectedList.Contains(collision.gameObject))
        {
            DetectedList.Add(collision.gameObject);
        }
        StartCoroutine(CheckCurrentGO());

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;

        //Item
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log($"Detected {collision.name} {this.name}");
            var collItem = collision.GetComponent<SOB_Item>();
            collItem.Detected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;
        
        if(currentGO == collision.gameObject)
        {
            currentGO = null;
        }

        DetectedList.Remove(collision.gameObject);

        //Item
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log($"UnDetected {collision.name}");
            var collItem = collision.GetComponent<SOB_Item>();
            collItem.UnDetected();
        }
    }
}

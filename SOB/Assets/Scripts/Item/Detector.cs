using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Item;
using SOB.CoreSystem;
using UnityEditor;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;

public class Detector : MonoBehaviour
{
    private Unit unit;
    private BoxCollider2D BC2D;

    [field: SerializeField]
    [field: Tooltip("설정한 LayerMask만 탐지가능")]
    public LayerMask DetectorMask { get; private set; }

    private List<GameObject> DetectedList = new List<GameObject>();
    [SerializeField]private GameObject currentGO;
    //private float currentDistance = 0.0f;
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

    private void FixedUpdate()
    {
        if (currentGO != null)
        {
            var player = unit as Player;
            if (player == null)
            {
                Debug.Log("player is null");
                return;
            }
            if (currentGO.tag == "Item")
            {
                if (player.InputHandler.InteractionInput)
                {
                    player.InputHandler.UseInput(ref player.InputHandler.InteractionInput);
                    Debug.Log($"{currentGO.transform.parent.name} is Add Inventory");
                    if (player.Inventory.AddInventoryItem(currentGO.transform.parent.gameObject))
                    {
                        Destroy(currentGO.transform.parent.gameObject);
                        currentGO = null;
                    }
                    else
                    {

                    }
                }
            }
            else if (currentGO.tag == "Interaction")
            {
                currentGO.GetComponent<InteractiveObject>().SetActiveBtnObj(true);
                if (player.InputHandler.InteractionInput)
                {
                    player.InputHandler.UseInput(ref player.InputHandler.InteractionInput);
                    Debug.Log($"{currentGO} interactive");
                    currentGO.GetComponent<InteractiveObject>()?.Interactive();
                }
            }

        }
    }
    IEnumerator CheckCurrentGO()
    {
        if (DetectedList.Count > 0)
        {
            foreach (GameObject go in DetectedList)
            {

                var player = unit as Player;
                if (currentGO == null)
                {
                    currentGO = go;
                    Debug.Log($"제일 가까운 오브젝트 {currentGO.transform.parent.name}");
                    if(currentGO.tag == "Item")
                    {
                        currentGO.GetComponentInParent<SOB_Item>().unit = unit;
                        currentGO.GetComponentInParent<SOB_Item>().Detected(this.gameObject.transform.position.x < currentGO.transform.position.x);
                    }
                    else if(go.tag == "Interaction")
                    {                        
                        currentGO.GetComponent<InteractiveObject>().SetActiveBtnObj(true);
                        if (player.InputHandler.InteractionInput)
                        {
                            player.InputHandler.UseInput(ref player.InputHandler.InteractionInput);
                            Debug.Log($"{currentGO} interactive");
                            currentGO.GetComponent<InteractiveObject>()?.Interactive();
                        }
                    }
                    
                    continue;
                }

                if (go == null)
                    continue;

                if (Vector2.Distance(currentGO.transform.position, this.gameObject.transform.position) > Vector2.Distance(go.transform.position, this.gameObject.transform.position))
                {
                    //이전 아이템 UnDetected
                    if (currentGO.tag =="Item")
                    {
                        currentGO.GetComponentInParent<SOB_Item>().UnDetected();
                    }
                    else if(currentGO.tag == "Interaction")
                    {
                        currentGO.GetComponent<InteractiveObject>().SetActiveBtnObj(false);
                        currentGO.GetComponent<InteractiveObject>().UnInteractive();
                    }
                    //가장 가까운 Detected 오브젝트
                    currentGO = go;
                    Debug.Log($"제일 가까운 오브젝트 {currentGO.transform.parent.name}");
                    if (currentGO.tag == "Item")
                    {
                        currentGO.GetComponentInParent<SOB_Item>().unit = unit;
                        currentGO.GetComponentInParent<SOB_Item>().Detected(this.gameObject.transform.position.x < currentGO.transform.position.x);
                    }
                    else if (go.tag == "Interaction")
                    {                        
                        currentGO.GetComponent<InteractiveObject>().SetActiveBtnObj(true);
                        if (player.InputHandler.InteractionInput)
                        {
                            player.InputHandler.UseInput(ref player.InputHandler.InteractionInput);
                            Debug.Log($"{currentGO} interactive");
                            currentGO.GetComponent<InteractiveObject>()?.Interactive();
                        }
                    }
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
        if (collision.tag == "Item")
        {
            var item = collision.GetComponentInParent<SOB_Item>();
            
            if (item == null)
                return;

            //EquipmentItem
            if (item.Item.effectData.isEquipment)
            {
                if (!DetectedList.Contains(collision.gameObject))
                {
                    DetectedList.Add(collision.gameObject);
                }
                StartCoroutine(CheckCurrentGO());
            }
        }
        //아이템이 아닌 상호작용 가능한 오브젝트
        else if(collision.tag == "Interaction")
        {
            if (!DetectedList.Contains(collision.gameObject))
            {
                DetectedList.Add(collision.gameObject);
            }
            StartCoroutine(CheckCurrentGO());
        }
            }
    //Detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;

        if (collision.tag == "Item")
        {
            var item = collision.GetComponentInParent<SOB_Item>();

            if (item == null)
                return;

            //Item
            if (item.Item.effectData.isEquipment)
            {
                //Debug.Log($"Detected {collision.name} {this.name}");
                //var collItem = collision.GetComponentInParent<SOB_Item>();
                //collItem.Detected();
            }
            //ConsumptionItem
            else
            {
                item.unit = unit;
                item.CallCoroutine(ItemGetType.Collision.ToString());
            }
        }

    }
    //Detected
    private void OnTriggerExit2D(Collider2D collision)
    {
        //DetectorMask 의 LayerMask가 아니면 return
        if ((DetectorMask.value & (1 << collision.gameObject.layer)) <= 0)
            return;

        DetectedList.Remove(collision.gameObject);

        if (collision.tag == "Item")
        {
            var item = collision.GetComponentInParent<SOB_Item>();
            if (item == null)
                return;
            //Item
            if (item.Item.effectData.isEquipment)
            {
                Debug.Log($"UnDetected {this.name}");
                if (currentGO == collision.gameObject)
                {
                    item.UnDetected();
                    currentGO = null;
                }
            }
        }
        else if(collision.tag == "Interaction")
        {
            currentGO.GetComponent<InteractiveObject>()?.SetActiveBtnObj(false);
            currentGO.GetComponent<InteractiveObject>()?.UnInteractive();
            currentGO = null;
        }
    }


    //Collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}

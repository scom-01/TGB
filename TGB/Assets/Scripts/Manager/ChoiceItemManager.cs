using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class ChoiceItemManager : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>();
    public Dictionary<StatsItemSO, Vector3> ItemDataList = new Dictionary<StatsItemSO, Vector3>();
    [HideInInspector]
    public int ChoiceItemAmount;
    private int CurrChoiceItemAmount = 0;

    [HideInInspector] public float Iteminterval = 0f;

    public bool isTriggerOn
    {
        get
        {
            return _isTriggerOn;
        }
        set
        {
            _isTriggerOn = value;
            DetectedArea.enabled = value;
        }
    }
    private bool _isTriggerOn = false;
    /// <summary>
    /// 아이템 스폰 여부
    /// </summary>
    private bool isSpawn = false;
    private BoxCollider2D DetectedArea;
    private void Awake()
    {
        CurrChoiceItemAmount = 0;

        DetectedArea = this.GetComponent<BoxCollider2D>();
        if (DetectedArea != null)
        {
            DetectedArea.isTrigger = true;
            DetectedArea.enabled = false;
            this.gameObject.layer = LayerMask.NameToLayer("Area");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawn)
            return;

        if (ItemList.Count == 0)
            return;

        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i] == null)
            {
                ItemList.RemoveAt(i);
                CurrChoiceItemAmount++;
            }
            if (CurrChoiceItemAmount >= ChoiceItemAmount)
            {
                DestroyChoiceItemList();
                break;
            }
        }
    }

    public bool AddSpawnItem(StatsItemSO itemData,Vector3 pos)
    {
        if (GameManager.Inst.StageManager.IM?.InventoryItem == null)
            return false;
        ItemDataList.Add(itemData, pos);
        return true;
    }

    private void ItemSpawn()
    {
        foreach (var itemData in ItemDataList)
        {
            var item = GameManager.Inst.StageManager.IM.SpawnItem(GameManager.Inst.StageManager.IM?.InventoryItem, itemData.Value, GameManager.Inst.StageManager?.IM?.transform, itemData.Key);
            ItemList.Add(item);
        }
        isSpawn = true;
    }

    private void DestroyChoiceItemList()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].gameObject != null)
                Destroy(ItemList[i].gameObject);
        }
        ItemList.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null)
            return;

        ItemSpawn();
    }
}

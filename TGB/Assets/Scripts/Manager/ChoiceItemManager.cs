using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class ChoiceItemManager : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>();
    [HideInInspector]
    public int ChoiceItemAmount;
    private int CurrChoiceItemAmount = 0;

    public float Iteminterval = 0f;

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
    private BoxCollider2D DetectedArea;

    private void Awake()
    {
        CurrChoiceItemAmount = 0;
        DetectedArea = this.GetComponent<BoxCollider2D>();
        DetectedArea.isTrigger = true;
        DetectedArea.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
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

    public bool AddSpawnItem(GameObject obj, Vector3 pos, Transform transform, StatsItemSO itemData)
    {
        if (obj = null)
            return false;

        var item = GameManager.Inst.StageManager.IM.SpawnItem(obj, pos, transform, itemData);
        if (item == null)
        {
            return false;
        }

        item.SetActive(false);
        ItemList.Add(item);

        return true;
    }

    private void ItemSpawn()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].SetActive(true);
        }
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

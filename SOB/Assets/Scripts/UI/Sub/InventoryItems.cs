using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public GameObject InventoryItemPrefab;
    public int MaxIndex;

    /// <summary>
    /// 현재 선택된 아이템
    /// </summary>
    public InventoryItem CurrentSelectItem
    {
        get => currentSelectItem;
        set
        {
            if (currentSelectItem != null)
                currentSelectItem.isSelect = false;

            currentSelectItem = value;
            currentSelectItem.isSelect = true;
        }
    }
    private InventoryItem currentSelectItem;

    /// <summary>
    /// 현재 선택된 아이템 인덱스
    /// </summary>
    public int CurrentSelectItemIndex
    {
        get => currentSelectItemIndex;
        set
        {
            currentSelectItemIndex = Mathf.Clamp(value, 0, items.Count - 1);
            CurrentSelectItem = items[currentSelectItemIndex];
            GameManager.Inst.SubUI.InventorySubUI.InventoryDescript.SetDescript();
        }
    }
    private int currentSelectItemIndex = 0;

    private void OnEnable()
    {
        //if (!(items.Count > 0))
        //{
        //    Init();
        //}
    }
    private void Init()
    {
        if(this.GetComponentsInChildren<InventoryItem>().Length == 0)
        {
            for (int i = 0; i < MaxIndex; i++)
            {
                var item = Instantiate(InventoryItemPrefab, this.transform).GetComponent<InventoryItem>();
                items.Add(item);
                item.Index = i;
            }
        }
        else if(this.GetComponentsInChildren<InventoryItem>().Length != MaxIndex)
        {
            for (int i = this.GetComponentsInChildren<InventoryItem>().Length; i < MaxIndex; i++)
            {
                var item = Instantiate(InventoryItemPrefab, this.transform).GetComponent<InventoryItem>();
                items.Add(item);
                item.Index = i;
            }
        }
    }

    public void AddItem(StatsItemSO StatsItem)
    {        
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].StatsItemData != null)
            {
                continue;
            }
            else
            {
                Debug.LogWarning($"{items[i].name}.StatsItemData is Null");
                items[i].StatsItemData = StatsItem;
                CurrentSelectItemIndex = i;
                items[i].Index = CurrentSelectItemIndex;
                break;
            }
        }
    }
    public void RemoveItem(StatsItemSO StatsItem)
    {
        foreach(var item in items)
        {
            if(item.StatsItemData == StatsItem)
            {
                for(int i = item.Index; i < items.Count-1; i++)
                {
                    items[i].StatsItemData = items[i + 1].StatsItemData;
                }
                items.Last().StatsItemData = null;
                return;
            }
        }
        //CurrentSelectItem.StatsItemData = null;
        //CurrentSelectItemIndex--;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
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
        foreach (var item in this.GetComponentsInChildren<InventoryItem>())
        {
            items.Add(item);
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
                item.StatsItemData = null;
                return;
            }
        }
        //CurrentSelectItem.StatsItemData = null;
        //CurrentSelectItemIndex--;
    }
}
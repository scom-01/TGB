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
    private int currentSelectItemIndex;

    private void Init()
    {
        foreach (var item in this.GetComponentsInChildren<InventoryItem>())
        {
            items.Add(item);
        }
    }

    public void AddItem(StatsItemSO StatsItem)
    {
        if (!(items.Count > 0))
        {
            Init();
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].StatsItemData != null)
            {
                continue;
            }
            else
            {
                Debug.LogWarning($"{items[i].name}.StatsItemData is Null");
                CurrentSelectItem = items[i];
                currentSelectItemIndex = i;
                items[i].StatsItemData = StatsItem;
                break;
            }
        }
    }
    public void RemoveItem(StatsItemSO StatsItem)
    {
        CurrentSelectItem.StatsItemData = null;
        CurrentSelectItem = items[currentSelectItemIndex--];
    }
}
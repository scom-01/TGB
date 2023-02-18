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
            currentSelectItemIndex = value;
            CurrentSelectItem = items[currentSelectItemIndex];
        }
    }
    private int currentSelectItemIndex = 0;

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
                currentSelectItemIndex = i;
                items[i].StatsItemData = StatsItem;
                break;
            }
        }
    }
    public void RemoveItem(StatsItemSO StatsItem)
    {
        items[currentSelectItemIndex].StatsItemData = null;
        CurrentSelectItem.StatsItemData = null;
        CurrentSelectItemIndex--;
    }
}
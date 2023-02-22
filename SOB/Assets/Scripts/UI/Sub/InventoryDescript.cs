using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InventoryDescript : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescript;
    public TextMeshProUGUI DropButton;

    public void OnEnable()
    {
        SetDescript();
    }

    public void SetDescript()
    {
        var item = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem;
        if (item != null && item.StatsItemData != null)
        {
            ItemName.text = item.StatsItemData.ItemName;
            ItemDescript.text = item.StatsItemData.ItemDescription;
            DropButton.enabled = true;
            return;
        }
        ItemName.text = "";
        ItemDescript.text = "";
        DropButton.enabled = false;
    }

    public void Drop()
    {
        var item = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem;
        if (item != null)
        {
            GameManager.Inst.player.Inventory.RemoveInventoryItem(item.StatsItemData);
            SetDescript();
        }
    }
}

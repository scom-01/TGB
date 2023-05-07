using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (ItemName != null) 
                ItemName.text = item.StatsItemData.ItemName;
            if (ItemDescript != null)
                ItemDescript.text = item.StatsItemData.ItemDescription;
            if (DropButton != null)
                DropButton.enabled = true;
            return;
        }
        if (ItemName != null)
            ItemName.text = "";
        if (ItemDescript != null)
            ItemDescript.text = "";
        if (DropButton != null)
            DropButton.enabled = false;
    }

    public void Drop()
    {
        var item = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem;
        if (item != null)
        {
            if (!GameManager.Inst.StageManager.player.Inventory.RemoveInventoryItem(item.StatsItemData))
                return;
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItemIndex--;
            EventSystem.current.SetSelectedGameObject(GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem.gameObject);
            SetDescript();
        }
    }
}

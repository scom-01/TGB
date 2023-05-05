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
            GameManager.Inst.StageManager.player.Inventory.RemoveInventoryItem(item.StatsItemData);
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItemIndex--;
            EventSystem.current.SetSelectedGameObject(GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem.gameObject);
            SetDescript();
        }
    }
}

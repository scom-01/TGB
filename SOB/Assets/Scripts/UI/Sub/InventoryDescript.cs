using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InventoryDescript : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescript;

    public void OnEnable()
    {
        if (GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem != null)
        {
            ItemName.text = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem.StatsItemData.ItemName;
            ItemDescript.text = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem.StatsItemData.ItemDescription;
        }
        else
        {
            ItemName.text = "";
            ItemDescript.text = "";
        }

    }
}

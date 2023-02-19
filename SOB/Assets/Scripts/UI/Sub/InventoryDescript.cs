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
        
    }

    public void SetDescript()
    {
        var item = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem;
        if (item != null && item.StatsItemData!=null)
        {
                ItemName.text = item.StatsItemData.ItemName;
                ItemDescript.text = item.StatsItemData.ItemDescription;
                return;
        }
        ItemName.text = "";
        ItemDescript.text = "";
    }
}

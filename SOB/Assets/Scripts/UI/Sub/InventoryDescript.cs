using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class InventoryDescript : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent ItemNameStringEvent;
    [SerializeField] private LocalizeStringEvent ItemDescriptStringEvent;
    public GameObject DropButton;
    public GameObject DropKeyButton;

    public void OnEnable()
    {
        SetDescript();
    }

    public void SetDescript()
    {
        var item = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.CurrentSelectItem;
        if (item != null && item.StatsItemData != null)
        {
            if (ItemNameStringEvent != null)
            {
                ItemNameStringEvent.StringReference.SetReference("Item_Table", item.StatsItemData.itemData.ItemNameLocal.TableEntryReference);                
            }
            
            if (ItemDescriptStringEvent != null)
            {
                ItemDescriptStringEvent.StringReference.SetReference("Item_Table", item.StatsItemData.itemData.ItemDescriptionLocal.TableEntryReference);
            }
            
            if (DropButton != null)
                DropButton.SetActive(true);
            if (DropKeyButton != null)
                DropKeyButton.SetActive(true);
            return;
        }
        if (ItemNameStringEvent != null)
        {
            ItemNameStringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (ItemDescriptStringEvent != null)
        {
            ItemDescriptStringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (DropButton != null)
            DropButton.SetActive(false);
        if (DropKeyButton != null)
            DropKeyButton.SetActive(false);
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

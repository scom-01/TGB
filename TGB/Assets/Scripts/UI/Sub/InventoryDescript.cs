using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class InventoryDescript : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent ItemName_StringEvent;
    [SerializeField] private LocalizeStringEvent ItemDescript_StringEvent;
    [SerializeField] private LocalizeStringEvent ItemEventName_StringEvent;
    [SerializeField] private LocalizeStringEvent ItemEventDescript_StringEvent;
    [SerializeField] private LocalizeStringEvent ItemLevel_StringEvent;
    [SerializeField] private TextMeshProUGUI ItemStatsTMP;
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
            if (ItemName_StringEvent != null && item.StatsItemData.itemData.ItemNameLocal.TableEntryReference.KeyId != 0)
            {
                ItemName_StringEvent.StringReference.SetReference("Item_Table", item.StatsItemData.itemData.ItemNameLocal.TableEntryReference);                
            }
            
            if (ItemDescript_StringEvent != null && item.StatsItemData.itemData.ItemDescriptionLocal.TableEntryReference.KeyId != 0)
            {
                ItemDescript_StringEvent.StringReference.SetReference("Item_Table", item.StatsItemData.itemData.ItemDescriptionLocal.TableEntryReference);
            }

            if (ItemEventName_StringEvent != null)
            {
                if (item.StatsItemData.EventNameLocal.TableEntryReference.KeyId != 0) ItemEventName_StringEvent.StringReference.SetReference("ItemEvent_Table", item.StatsItemData.EventNameLocal.TableEntryReference);
                else ItemEventName_StringEvent.StringReference.SetReference("Item_Table", "Empty");
            }
            
            if (ItemEventDescript_StringEvent != null)
            {
                if (item.StatsItemData.EventDescriptionLocal.TableEntryReference.KeyId != 0) ItemEventDescript_StringEvent.StringReference.SetReference("ItemEvent_Descript_Table", item.StatsItemData.EventDescriptionLocal.TableEntryReference);
                else ItemEventDescript_StringEvent.StringReference.SetReference("Item_Table", "Empty");
            }

            if (ItemLevel_StringEvent != null)
            {
                ItemLevel_StringEvent.StringReference.SetReference("UI_Table", item.StatsItemData.itemData.ItemLevel.ToString());
            }

            if (ItemStatsTMP != null)
            {
                if(item.StatsItemData.StatsItems.Count > 0)
                {
                    ItemStatsTMP.text = item.StatsItemData.StatsData_Descripts;
                }
                else
                {
                    ItemStatsTMP.text = "";
                }
            }
            
            if (DropButton != null)
                DropButton.SetActive(true);
            if (DropKeyButton != null)
                DropKeyButton.SetActive(true);
            return;
        }
        if (ItemName_StringEvent != null)
        {
            ItemName_StringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (ItemDescript_StringEvent != null)
        {
            ItemDescript_StringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (ItemEventName_StringEvent != null)
        {
            ItemEventName_StringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (ItemEventDescript_StringEvent != null)
        {
            ItemEventDescript_StringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (ItemLevel_StringEvent != null)
        {
            ItemLevel_StringEvent.StringReference.SetReference("Item_Table", "Empty");
        }
        if (ItemStatsTMP != null)
        {
            ItemStatsTMP.text = "";
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

using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class CurrItem : MonoBehaviour, IUI_Select
{
    [SerializeField] private Image IconImg;
    [SerializeField] private LocalizeStringEvent Local_Name;
    [SerializeField] private LocalizeStringEvent Local_Descript;
    private StatsItemSO m_itemSO;

    private void Start()
    {
        if (m_itemSO == null)
        {
            if (IconImg != null)
                IconImg.enabled = false;

            if (Local_Descript != null)
                Local_Descript.StringReference.SetReference("Item_Table", "Empty");

            if (Local_Name != null)
                Local_Name.StringReference.SetReference("Item_Table", "Empty");
        }
    }

    public void Select(GameObject go)
    {
        if (go == null)
            return;

        if (go.GetComponent<InventoryItem>() != null)
        {
            m_itemSO = go.GetComponent<InventoryItem>().StatsItemData;

            if (m_itemSO == null)
            {
                if (IconImg != null)
                    IconImg.enabled = false;

                if (Local_Descript != null)
                    Local_Descript.StringReference.SetReference("Item_Table", "Empty");

                if (Local_Name != null)
                    Local_Name.StringReference.SetReference("Item_Table", "Empty");
            }
            else
            {
                if (IconImg != null)
                {
                    IconImg.enabled = true;
                    IconImg.sprite = m_itemSO.itemData.ItemSprite;
                }

                if (Local_Descript != null)
                    Local_Descript.StringReference.SetReference("Item_Table", m_itemSO.itemData.ItemDescriptionLocal.TableEntryReference);

                if (Local_Name != null)
                    Local_Name.StringReference.SetReference("Item_Table", m_itemSO.itemData.ItemNameLocal.TableEntryReference);
            }
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log("currentSelectedGameObject = " + EventSystem.current.currentSelectedGameObject.name);
        }
        else
        {

            Debug.Log("CurrentSelectedGameObject = null");
        }
    }
}

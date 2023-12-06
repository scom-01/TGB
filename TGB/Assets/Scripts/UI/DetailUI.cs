using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DetailUI : MonoBehaviour
{
    [Header("---Main---")]
    [SerializeField]
    private GameObject mainUI;
    [SerializeField] private LocalizeStringEvent MainStringEvent;
    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }
            return canvas;
        }
    }
    private Canvas canvas;

    [SerializeField] private Image Icon;
    [SerializeField] private LocalizeStringEvent ItemLevel_StringEvent;

    [Header("---Sub---")]
    [SerializeField]
    private GameObject subUI;
    [SerializeField] private LocalizeStringEvent SubStringEvent;
    [SerializeField] private TextMeshProUGUI StatsDescript;

    [Header("---Event---")]
    [SerializeField] private LocalizeStringEvent Event_Name;
    [SerializeField] private LocalizeStringEvent Event_Descript;
    [Tooltip("하위 컴포넌트 중 'SubText' Text String")]

    public void SetInit(StatsItemSO item)
    {
        if (MainStringEvent != null && item.itemData.ItemNameLocal != null)
        {
            MainStringEvent.StringReference.SetReference("Item_Table", item.itemData.ItemNameLocal.TableEntryReference);
        }

        if (SubStringEvent != null && item.itemData.ItemDescriptionLocal != null)
        {
            SubStringEvent.StringReference.SetReference("Item_Table", item.itemData.ItemDescriptionLocal.TableEntryReference);
        }

        if (ItemLevel_StringEvent != null)
        {
            ItemLevel_StringEvent.StringReference.SetReference("UI_Table", item.itemData.ItemLevel.ToString());
        }

        if (Event_Name != null && item.EventNameLocal != null)
        {
            if (item.EventNameLocal.TableEntryReference.KeyId != 0) Event_Name.StringReference.SetReference("ItemEvent_Table", item.EventNameLocal.TableEntryReference);
            else Event_Name.StringReference.SetReference("Item_Table", "Empty");
        }

        if (Event_Descript != null && item.EventDescriptionLocal != null)
        {
            if (item.EventDescriptionLocal.TableEntryReference.KeyId != 0) Event_Descript.StringReference.SetReference("ItemEvent_Table", item.EventDescriptionLocal.TableEntryReference);
            else Event_Descript.StringReference.SetReference("Item_Table", "Empty");
        }

        if (StatsDescript != null)
        {
            StatsDescript.text = "";
            StatsDescript.text = item.StatsData_Descripts;
        }

        if (Icon != null)
        {
            Icon.sprite = item.itemData.ItemSprite;
        }
    }
}
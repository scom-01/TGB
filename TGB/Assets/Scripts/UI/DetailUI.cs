using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("---Button---")]
    [SerializeField] private Image HoldFilledImg;

    [Header("---Item---")]
    /// <summary>
    /// 표기 아이템
    /// </summary>
    public StatsItemSO item;
    /// <summary>
    /// 맵 화면상에 드랍되어있는 아이템.게임오브젝트(줍거나 팔기시 사라져야 하기에)
    /// </summary>
    [HideInInspector] public GameObject GO;


    public Player player => GameManager.Inst?.StageManager?.player;

    public void SetInit(StatsItemSO _item, GameObject gameObject)
    {
        this.item = _item;
        if (SetUI(item))
        {
            this.GO = gameObject;
        }
    }

    private bool SetUI(StatsItemSO _item)
    {
        if (_item == null)
            return false;

        if (MainStringEvent != null && _item.itemData.ItemNameLocal != null)
        {
            MainStringEvent.StringReference.SetReference("Item_Table", _item.itemData.ItemNameLocal.TableEntryReference);
        }

        if (SubStringEvent != null && _item.itemData.ItemDescriptionLocal != null)
        {
            SubStringEvent.StringReference.SetReference("Item_Table", _item.itemData.ItemDescriptionLocal.TableEntryReference);
        }

        if (ItemLevel_StringEvent != null)
        {
            ItemLevel_StringEvent.StringReference.SetReference("UI_Table", _item.itemData.ItemLevel.ToString());
        }

        if (Event_Name != null && _item.EventNameLocal != null)
        {
            if (_item.EventNameLocal.TableEntryReference.KeyId != 0) Event_Name.StringReference.SetReference("ItemEvent_Table", _item.EventNameLocal.TableEntryReference);
            else Event_Name.StringReference.SetReference("Item_Table", "Empty");
        }

        if (Event_Descript != null && _item.EventDescriptionLocal != null)
        {
            if (_item.EventDescriptionLocal.TableEntryReference.KeyId != 0) Event_Descript.StringReference.SetReference("ItemEvent_Descript_Table", _item.EventDescriptionLocal.TableEntryReference);
            else Event_Descript.StringReference.SetReference("Item_Table", "Empty");
        }

        if (StatsDescript != null)
        {
            StatsDescript.text = "";
            StatsDescript.text = _item.StatsData_Descripts;
        }

        if (Icon != null)
        {
            Icon.sprite = _item.itemData.ItemSprite;
        }

        if (HoldFilledImg != null)
        {
            HoldFilledImg.fillAmount = 0;
        }

        return true;
    }

    public void LateUpdate()
    {
        if (player == null)
            return;

        if (GO == null)
            return;

        if (player.InputHandler.InteractionInput)
        {
            Debug.Log($"interactionMaxTapDuration = {player.InputHandler.interactionMaxTapDuration},  " +
                $"interactionMaxHoldDuration = {player.InputHandler.interactionMaxHoldDuration},  " +
                $"filledImg = {(Time.time - player.InputHandler.interactionInputStartTime) / (player.InputHandler.interactionMaxHoldDuration - InputSystem.settings.defaultTapTime)}");
            //Tap Duration보단 길게
            if (player.InputHandler.interactionMaxHoldDuration > 0f)
            {
                if (HoldFilledImg != null)
                {
                    HoldFilledImg.fillAmount = (Time.time - player.InputHandler.interactionInputStartTime) / (player.InputHandler.interactionMaxHoldDuration - InputSystem.settings.defaultTapTime);
                }
                if (player.InputHandler.interactionperformed)
                {
                    Debug.Log("Sell Item");
                    player.InputHandler.UseInput(ref player.InputHandler.InteractionInput);
                    player.InputHandler.UseInput(ref player.InputHandler.interactionperformed);
                    return;
                }
            }            
            if (player.InputHandler.interactionperformed)
            {
                player.InputHandler.UseInput(ref player.InputHandler.InteractionInput);
                player.InputHandler.UseInput(ref player.InputHandler.interactionperformed);
                if (HoldFilledImg != null)
                {
                    HoldFilledImg.fillAmount = 0f;
                }
                if (player.Inventory.AddInventoryItem(GO))
                {
                    Destroy(GO);
                    return;
                }
            }
        }
        else
        {
            if (HoldFilledImg != null)
            {
                HoldFilledImg.fillAmount = 0f;
            }
        }
    }
}
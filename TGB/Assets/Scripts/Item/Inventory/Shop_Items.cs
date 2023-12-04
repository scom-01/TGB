using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;

public class Shop_Items : InventoryItems, IUI_Select
{
    [SerializeField] private TMP_Text reRollTxt;
    private int ReRollCount = 1;

    private StatsItemSO SelectedItem;
    private int SelectedIdx;
    [SerializeField] private Merchant Merchant;
    [SerializeField] private LocalizeStringEvent NotEnoughMessage_Local;
    [SerializeField] private Animator NotEnoughMessage_Anim;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        Merchant = this.GetComponentInParent<Merchant>();

        if (reRollTxt != null)
            reRollTxt.text = string.Format("{0:#,###}", (GameManager.Inst.StageManager.StageLevel * GlobalValue.ReRoll_Inflation * ReRollCount)) + "<color=yellow>G</color>";
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].StatsItemData = null;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            ChangeItem(i);
        }
    }
    /// <summary>
    /// 아이템 구매 UI Button Event에서 호출
    /// </summary>
    public void Buy()
    {
        if (GameManager.Inst?.StageManager?.player == null || SelectedItem == null)
            return;

        if (DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.Goods.Gold < (int)SelectedItem.itemData.ItemLevel * GlobalValue.Gold_Inflation * GameManager.Inst.StageManager.StageLevel)
        {
            //재화 부족
            if (NotEnoughMessage_Anim != null && NotEnoughMessage_Local != null) 
            {
                NotEnoughMessage_Local.StringReference.SetReference("Goods_Table", GOODS_TPYE.Gold.ToString() + "_NotEnough");
                NotEnoughMessage_Anim.Play("Action", -1, 0f);
            }
            return;
        }
        else
        {
            DataManager.Inst.CalculateGoods(GOODS_TPYE.Gold, -(int)SelectedItem.itemData.ItemLevel * GlobalValue.Gold_Inflation * GameManager.Inst.StageManager.StageLevel);
        }

        if (GameManager.Inst.StageManager.player.Inventory.AddInventoryItem(SelectedItem))
        {
            //Inventory Item Setup
            for (int i = 0; i < Merchant.Inventoryitems.Items.Count; i++)
            {
                if (i < GameManager.Inst.StageManager?.player.Inventory.Items.Count)
                {
                    Merchant.Inventoryitems.Items[i].StatsItemData = GameManager.Inst.StageManager?.player.Inventory.Items[i].item;
                }
                else
                {
                    Merchant.Inventoryitems.Items[i].StatsItemData = null;
                }
            }

            if(!ChangeItem(SelectedIdx))
            {
                Items[SelectedIdx].StatsItemData = null;
            }
            GameManager.Inst.SetSelectedObject(Items[SelectedIdx].gameObject);
            EventSystem.current.SetSelectedGameObject(Items[SelectedIdx].gameObject);
            //아이템 리롤
        }
        else
        {
            //spawnItem
            GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, GameManager.Inst.StageManager.player.transform.position, GameManager.Inst.StageManager.IM.transform, SelectedItem);
        }
    }
    /// <summary>
    /// 상점아이템 리롤
    /// </summary>
    /// <param name="idx">리롤할 상점 아이템 Idx</param>
    private bool ChangeItem(int idx)
    {
        if (GameManager.Inst?.StageManager?.player == null)
        {
            Items[idx].StatsItemData = null;
            return false;
        }

        //인벤토리 중복 제거
        var list = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.ToList();
        for (int i = 0; i < GameManager.Inst.StageManager.player.Inventory.Items.Count; i++)
        {
            if(list.Contains(GameManager.Inst.StageManager.player.Inventory.Items[i].item.ItemIdx))
            {
                list.Remove(GameManager.Inst.StageManager.player.Inventory.Items[i].item.ItemIdx);
            }
        }

        //필드 드랍아이템이 아닐 시 제외
        for (int j = 0; j < list.Count; j++)
        {
            if (DataManager.Inst.All_ItemDB.ItemDBList[list[j]].isFieldSpawn == false)
            {
                list.RemoveAt(j);
            }
        }

        if (list.Count == 0)
        {
            Items[idx].StatsItemData = null;
            return false;
        }

        int Itemidx = UnityEngine.Random.Range(0, list.Count);
        //DB에서 찾을 아이템 Index
        int ItemNum = list[Itemidx];

        //다른 상점아이템 리스트와의 중복 제거
        for (int i = 0; i < Items.Count;)
        {
            if (Items[i] == Items[idx])
            {
                i++;
                continue;
            }

            if (Items[i].StatsItemData == DataManager.Inst.All_ItemDB.ItemDBList[ItemNum])
            {                
                list.Remove(ItemNum);
                if(list.Count == 0)
                {
                    Items[idx].StatsItemData = null;
                    return false;
                }                
                Itemidx = UnityEngine.Random.Range(0, list.Count);                
                ItemNum = list[Itemidx];
                i = 0;
                continue;
            }
            i++;
        }
        Items[idx].StatsItemData = DataManager.Inst.All_ItemDB.ItemDBList[ItemNum];
        return true;
    }


    /// <summary>
    /// UI Button으로 호출되는 함수
    /// </summary>
    public void ReRoll()
    {
        //보유 재화 체크
        if (DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.Goods.Gold < (GameManager.Inst.StageManager.StageLevel * GlobalValue.ReRoll_Inflation * ReRollCount))
        {
            //재화 부족
            if (NotEnoughMessage_Anim != null && NotEnoughMessage_Local != null)
            {
                NotEnoughMessage_Local.StringReference.SetReference("Goods_Table", GOODS_TPYE.Gold.ToString() + "_NotEnough");
                NotEnoughMessage_Anim.Play("Action", -1, 0f);
            }
            return;
        }
        else
        {
            DataManager.Inst.CalculateGoods(GOODS_TPYE.Gold, -(GameManager.Inst.StageManager.StageLevel * GlobalValue.ReRoll_Inflation * ReRollCount));            
        }
        if (reRollTxt != null)
        {
            ReRollCount++;
            reRollTxt.text = (GameManager.Inst.StageManager.StageLevel * GlobalValue.ReRoll_Inflation * ReRollCount) + "<color=yellow>G</color>";
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].StatsItemData = null;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            ChangeItem(i);
        }

        return;
    }

    public void Select(GameObject go)
    {
        if (go.GetComponent<InventoryItem>() == null)
            return;

        SelectedIdx = go.GetComponent<InventoryItem>().Index;
        SelectedItem = go.GetComponent<InventoryItem>().StatsItemData;
    }
}

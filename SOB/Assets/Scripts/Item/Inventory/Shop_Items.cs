using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Shop_Items : InventoryItems, IUI_Select
{
    private int ReRollCount = 0;

    private StatsItemSO SelectedItem;
    private int SelectedIdx;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        ReRoll();
    }
    /// <summary>
    /// 아이템 구매
    /// </summary>
    public void Buy()
    {
        if (GameManager.Inst?.StageManager?.player == null)
            return;

        if (DataManager.Inst.JSON_DataParsing.m_JSON_Goods.gold < (int)SelectedItem.ItemLevel * 250 * GameManager.Inst.StageManager.StageLevel)
        {
            //재화 부족
            return;
        }

        if (GameManager.Inst.StageManager.player.Inventory.AddInventoryItem(SelectedItem))
        {
            ChangeItem(SelectedIdx);
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
    /// <param name="idx">리롤할 상점 Idx</param>
    private void ChangeItem(int idx)
    {
        int Itemidx = UnityEngine.Random.Range(0, DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.Count);

        foreach(var item in Items)
        {
            if (item.StatsItemData == DataManager.Inst.All_ItemDB.ItemDBList[Itemidx])
                return;
        }

        Items[idx].StatsItemData = DataManager.Inst.All_ItemDB.ItemDBList[Itemidx];
    }

    public void ReRoll()
    {
        //상점 아이템리스트 만큼 아이템이 없으면 중지
        if(MaxIndex > DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.Count)
        {
            return;
        }

        //보유 재화 체크
        if(DataManager.Inst.JSON_DataParsing.m_JSON_Goods.gold < GameManager.Inst.StageManager.StageLevel * 60 * ReRollCount)
        {
            return;
        }

        for (int i = 0; i<Items.Count;i++)
        {
            ChangeItem(i);
        }

        return;
    }

    public void Select(GameObject go)
    {
        if (go.GetComponent<StatsItemSO>() == null)
            return;

        SelectedIdx = go.GetComponent<InventoryItem>().Index;
        SelectedItem = go.GetComponent<InventoryItem>().StatsItemData;
    }
}

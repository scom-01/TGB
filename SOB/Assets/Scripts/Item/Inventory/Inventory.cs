using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;
using SOB.Item;
using Unity.VisualScripting;
using SOB.CoreSystem;

public class Inventory : MonoBehaviour
{
    private Unit unit;
    public Weapon[] weapons;
    public List<StatsItemSO> items;

    private int ItemCount;

    private void Awake()
    {
        unit = this.GetComponent<Unit>();
        ItemCount = items.Count;
    }
    private void Start()
    {
        if(weapons == null || weapons?.Length == 0)
        {
            weapons = this.GetComponentsInChildren<Weapon>();
            if (weapons == null)
            {
                Debug.LogWarning($"{transform.name}'s Weapons is empty in The Inventory");
            }
        }

        if(items == null || items?.Count == 0)
        {
            if (items == null)
            {
                Debug.LogWarning($"{transform.name}'s Items is empty in The Inventory");
            }
        }
    }

    private void Update()
    {
        var oldWeapon = weapons;
        if(weapons != oldWeapon)
        {
            ChangeWeaponAttribute();
        }

        if(items.Count != ItemCount)
        {

            ChangeItemAttribute();
        }
    }

    public bool AddInventoryItem(StatsItemSO itemData)
    {
        if(items.Count >= 8)
        {
            Debug.LogWarning("Inventory is full");
            //아이템 교체하는 코드
            return false;
        }

        //중복금지
        if (items.Contains(itemData))
        {
            Debug.Log($"Contians {itemData.name}, fail add");
        }
        else
        {
            Debug.Log($"Add {itemData.name}, Success add {itemData.name}");
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.AddItem(itemData);
            items.Add(itemData);
            ItemCount++;
            foreach (var commonData in itemData.StatsDatas)
            {
                SetStat(commonData);
            }                
            Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().StatsData}");
        }
        return true;
    }

    public void RemoveInventoryItem(StatsItemSO itemData)
    {
        if(items.Contains(itemData))
        {
            Debug.Log($"Remove Item {itemData.name}");
            foreach (var commonData in itemData.StatsDatas)
            {
                SetStat(commonData * -1f);
            }
            items.Remove(itemData);
        }
        else
        {
            Debug.Log($"Not Contians {itemData.name}, fail remove");
        }
    }

    
    private void ChangeWeaponAttribute()
    {

    }

    private void ChangeItemAttribute()
    {

    }

    /// <summary>
    /// Unit의 CommonData Stats 변경
    /// </summary>
    /// <param name="statsData"></param>
    /// <param name="plus">Plus = true, Minus = false</param>
    private void SetStat(StatsData statsData)
    {
        unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData;
    }
}

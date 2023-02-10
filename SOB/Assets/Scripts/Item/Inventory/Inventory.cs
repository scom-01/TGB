using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;
using SOB.Item;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using SOB.CoreSystem;

public class Inventory : MonoBehaviour
{
    private Unit unit;
    public Weapon[] weapons;
    public List<ItemDataSO> items;

    private void Awake()
    {
        unit = this.GetComponent<Unit>();
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

        var oldItem = items;
        if(items != oldItem)
        {
            ChangeItemAttribute();
        }
    }

    public void AddInventoryItem(ItemDataSO itemData)
    {
        //중복금지
        if (items.Contains(itemData))
        {
            Debug.Log($"Contians {itemData.name}, fail add");
        }
        else
        {
            Debug.Log($"Add {itemData.name}, Success add {itemData.name}");
            items.Add(itemData);

            foreach(var commonData in itemData.CommonDatas)
            {
                SetStat(commonData);
            }                
            Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().CommonData}");
        }
    }

    public void RemoveInventoryItem(ItemDataSO itemData)
    {
        if(items.Contains(itemData))
        {
            Debug.Log($"Remove Item {itemData.name}");
            foreach (var commonData in itemData.CommonDatas)
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
        //CalculateItem(items);
    }

    /// <summary>
    /// Unit의 CommonData Stats 변경
    /// </summary>
    /// <param name="commonData"></param>
    /// <param name="plus">Plus = true, Minus = false</param>
    private void SetStat(CommonData commonData)
    {
        unit.Core.GetCoreComponent<UnitStats>().CommonData += commonData;
    }
}

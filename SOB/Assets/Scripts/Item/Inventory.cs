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
    public Unit unit;
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
            //foreach(var item in this.GetComponentsInChildren<SOB_Item>())
            //{
            //    items.Add(item.itemData);
            //}

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

    public void AddInventoryItem(ItemDataSO item)
    {
        if (items.Contains(item))
        {
            Debug.Log($"Contians {item.name}, fail add");
        }
        else
        {
            Debug.Log($"Add {item.name}, Success add {item.name}");
            items.Add(item);
                        
            unit.Core.GetCoreComponent<UnitStats>().CommonData += item.ItemData.commomData;
            Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().CommonData}");
        }
    }

    public void RemoveInventoryItem(ItemDataSO item)
    {
        if(items.Contains(item))
        {
            Debug.Log($"Remove Item {item.name}");
            items.Remove(item);
            unit.Core.GetCoreComponent<UnitStats>().CommonData -= item.ItemData.commomData;
        }
        else
        {
            Debug.Log($"Not Contians {item.name}, fail remove");
        }
    }
    private void ChangeWeaponAttribute()
    {

    }

    private void ChangeItemAttribute()
    {
        //CalculateItem(items);
    }

    private void CalculateItem(List<ItemDataSO> items)
    {
        //item들의 CommonData합산
        foreach(var item in items)
        {
            unit.Core.GetCoreComponent<UnitStats>().CommonData += item.ItemData.commomData;
        }
    }
}

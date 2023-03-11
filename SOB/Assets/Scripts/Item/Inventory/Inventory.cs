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
    public List<Weapon> weapons = new List<Weapon>();
    public List<StatsItemSO> items = new List<StatsItemSO>();

    private int ItemCount;

    private void Awake()
    {
        
    }
    private void Start()
    {
        unit = this.GetComponent<Unit>();

        if (weapons == null || weapons?.Count == 0)
        {
            weapons = new List<Weapon>();
            var weaponItems = this.GetComponentsInChildren<Weapon>();
            for (int i = 0; i < weaponItems.Length; i++)
            {
                this.weapons.Add(weaponItems[i]);
            }

            if (this.weapons == null)
            {
                Debug.LogWarning($"{transform.name}'s Weapons is empty in The Inventory");
            }
        }

        if (items == null || items?.Count == 0)
        {
            Debug.LogWarning($"{transform.name}'s Items is empty in The Inventory");
            items = new List<StatsItemSO>();
        }
        ItemCount = items.Count;
    }

    private void Update()
    {
        var oldWeapon = weapons;
        if (weapons != oldWeapon)
        {
            ChangeWeaponAttribute();
        }

        if (items.Count != ItemCount)
        {

            ChangeItemAttribute();
        }
    }

    public bool AddInventoryItem(StatsItemSO itemData)
    {
        if (items.Count >= 8)
        {
            Debug.LogWarning("Inventory is full");
            //아이템 교체하는 코드
            return false;
        }

        //중복금지
        if (items.Contains(itemData))
        {
            Debug.Log($"Contians {itemData.name}, fail add");
            return false;
        }
        else
        {
            Debug.Log($"Add {itemData.name}, Success add {itemData.name}");
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.AddItem(itemData);
            items.Add(itemData);

            ItemCount++;
            foreach (var statsData in itemData.StatsDatas)
            {
                SetStat(statsData);
                if (statsData.MaxHealth != 0.0f)
                {
                    unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += statsData.MaxHealth;
                }
            }
            Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().StatsData}");
        }
        return true;
    }

    /// <summary>
    /// Item 제거, Destory하지 않고 InventoryItem의 StatsItemSO 데이터값을 없앤다.
    /// </summary>
    /// <param name="itemData"></param>
    public void RemoveInventoryItem(StatsItemSO itemData)
    {
        if (itemData == null)
        {
            Debug.Log("Find not InventoryItem");
            return;
        }

        if (items.Contains(itemData))
        {
            Debug.Log($"Remove Item {itemData.name}");
            foreach (var statsData in itemData.StatsDatas)
            {
                SetStat(statsData * -1f);
                if (statsData.MaxHealth != 0.0f)
                {
                    unit.Core.GetCoreComponent<UnitStats>().CurrentHealth -= statsData.MaxHealth;
                }
            }
            items.Remove(itemData);
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.RemoveItem(itemData);

            //spawnItem
            GameManager.Inst.SPM.SpawnItem(GameManager.Inst.IM.InventoryItem, unit.transform.position,GameManager.Inst.IM.transform, itemData);
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

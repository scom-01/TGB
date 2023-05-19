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
    public WeaponData weaponData;
    public Weapon Weapon
    {
        get
        {
            if (m_weapon == null)
            {
                m_weapon = this.GetComponentInChildren<Weapon>();
            }
            return m_weapon;
        }
        set
        {
            m_weapon = value;
        }
    }

    public List<StatsItemSO> items = new List<StatsItemSO>();
    public GameObject CheckItem;

    private Weapon m_weapon;
    private int ItemCount;
    private void Awake()
    {
        
    }
    private void Start()
    {
        unit = this.GetComponent<Unit>();
        weaponData = Weapon.weaponData;
        if (items == null || items?.Count == 0)
        {
            Debug.LogWarning($"{transform.name}'s Items is empty in The Inventory");
            items = new List<StatsItemSO>();
        }
        ItemCount = items.Count;
    }

    private void Update()
    {
        var oldWeapon = Weapon;
        if (Weapon != oldWeapon)
        {
            ChangeWeaponAttribute();
        }

        if (items.Count != ItemCount)
        {
            ChangeItemAttribute();
        }

        ItemExeUpdate(unit);
    }

    public bool ItemEffectExecute(Unit unit)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].ExeUse(unit);
        }
        return true;
    }
    public bool ItemEffectExecute(Unit unit, Unit Enemy)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].ExeUse(unit, Enemy);
        }
        return true;
    }

    public bool ItemExeUpdate(Unit unit)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].ExeUpdate(unit);
        }
        return true;
    }


    public bool SetWeapon(WeaponData weaponObject)
    {
        if(this.weaponData.weaponCommandDataSO == weaponObject.weaponCommandDataSO)
        {
            return false;
        }

        this.Weapon.SetData(weaponObject.weaponDataSO);
        this.Weapon.SetCommandData(weaponObject.weaponCommandDataSO);
        weaponData = weaponObject;
        
        return true;
    }

    public bool AddInventoryItem(GameObject itemObject)
    {
        StatsItemSO itemData = itemObject.GetComponent<SOB_Item>().Item;
        if (items.Count >= 8)
        {
            CheckItem = itemObject;
            Debug.LogWarning("Inventory is full");

            GameManager.Inst.SubUI.InventorySubUI.ChangeInventoryItem();
            GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.UI, true);
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
            SetStat(itemData.StatsDatas);
            if (itemData.StatsDatas.MaxHealth != 0.0f)
            {
                unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += itemData.StatsDatas.MaxHealth;
            }
            Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().StatsData}");            
        }
        return true;
    }

    /// <summary>
    /// Item 제거, Destory하지 않고 InventoryItem의 StatsItemSO 데이터값을 없앤다.
    /// </summary>
    /// <param name="itemData"></param>
    public bool RemoveInventoryItem(StatsItemSO itemData)
    {
        if (itemData == null)
        {
            Debug.Log("Find not InventoryItem");
            return false;
        }

        if (items.Contains(itemData))
        {
            Debug.Log($"Remove Item {itemData.name}");
            SetStat(itemData.StatsDatas * -1f);
            if (itemData.StatsDatas.MaxHealth != 0.0f)
            {
                unit.Core.GetCoreComponent<UnitStats>().CurrentHealth -= itemData.StatsDatas.MaxHealth;
            }
            items.Remove(itemData);
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.RemoveItem(itemData);

            //spawnItem
            GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, unit.transform.position, GameManager.Inst.StageManager.IM.transform, itemData);
        }
        else
        {
            Debug.Log($"Not Contians {itemData.name}, fail remove");
        }
        return true;
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

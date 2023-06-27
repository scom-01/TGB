using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;
using SOB.Item;
using Unity.VisualScripting;
using SOB.CoreSystem;
using System;
using TMPro;
using UnityEngine.Localization.SmartFormat.Utilities;

[Serializable]
public struct ItemSet
{
    public StatsItemSO item;
    public float startTime;
    public int attackCount;
    public ItemSet(StatsItemSO itemSO, float startTime = 0, int attackCount = 0)
    {
        this.item = itemSO;
        this.startTime = startTime;
        this.attackCount = attackCount;
    }
}
public class Inventory : MonoBehaviour
{
    public Unit Unit
    {
        get
        {
            if (unit == null)
            {
                unit = this.GetComponent<Unit>();
            }
            return unit;
        }
    }
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

    //public List<StatsItemSO> items = new List<StatsItemSO>();
    public List<ItemSet> _items = new List<ItemSet>();
    public List<StatsItemSO> Inititems = new List<StatsItemSO>();
    public GameObject CheckItem;

    private Weapon m_weapon;
    private int ItemCount;
    private void Start()
    {
        weaponData = Weapon.weaponData;
        if (_items == null || _items?.Count == 0)
        {
            Debug.LogWarning($"{transform.name}'s Items is empty in The Inventory");
            _items = new List<ItemSet>();
        }

        if (Inititems.Count != 0)
        {
            foreach (var item in Inititems)
            {
                ItemSet _item = new ItemSet(item, Time.time);
                _items.Add(_item);
            }
        }

        ItemCount = _items.Count;
    }

    private void Update()
    {
        var oldWeapon = Weapon;
        if (Weapon != oldWeapon)
        {
            ChangeWeaponAttribute();
        }

        if (_items.Count != ItemCount)
        {
            ChangeItemAttribute();
        }

        ItemExeUpdate(Unit);
    }

    public bool ItemEffectExecute(Unit unit)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            for (int j = 0; j < _items[i].item.ItemEffects.Count; j++)
            {
                var temp = _items[i].item.ExeUse(unit, _items[i].item.ItemEffects[j], _items[i].attackCount);
                ItemSet tempItem = new ItemSet(_items[i].item, _items[i].startTime, temp);
                _items[i] = tempItem;
            }
        }
        return true;
    }
    public bool ItemEffectExecute(Unit unit, Unit Enemy)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            for (int j = 0; j < _items[i].item.ItemEffects.Count; j++)
            {
                var temp = _items[i].item.ExeUse(unit, Enemy, _items[i].item.ItemEffects[j], _items[i].attackCount);
                ItemSet tempItem = new ItemSet(_items[i].item, _items[i].startTime, temp);
                _items[i] = tempItem;
            }
        }
        return true;
    }

    public bool ItemExeUpdate(Unit unit)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            for (int j = 0; j < _items[i].item.ItemEffects.Count; j++)
            {
                var temp = _items[i].item.ExeUpdate(unit, _items[i].item.ItemEffects[j], _items[i].startTime);
                ItemSet tempItem = new ItemSet(_items[i].item, temp, _items[i].attackCount);
                _items[i] = tempItem;
            }
        }
        return true;
    }


    public bool SetWeapon(WeaponData weaponObject)
    {
        if (this.weaponData.weaponCommandDataSO == weaponObject.weaponCommandDataSO)
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
        if (_items.Count >= 8)
        {
            CheckItem = itemObject;
            Debug.LogWarning("Inventory is full");

            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.SubUI.InventorySubUI.ChangeInventoryItem();
            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.UI, true);
            //아이템 교체하는 코드
            return false;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].item == itemData)
            {
                Debug.Log($"Contians {itemData.name}, fail add");
                return false;
            }
        }

        ////중복금지
        //if (_items.Contains(itemData))
        //{

        //}
        //else
        //{
        Debug.Log($"Add {itemData.name}, Success add {itemData.name}");
        if (unit.GetType() == typeof(Player))
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.AddItem(itemData);

        ItemSet item = new ItemSet(itemData, Time.time);
        _items.Add(item);

        ItemCount++;
        AddStat(itemData.StatsDatas);
        //if (itemData.StatsDatas.MaxHealth != 0.0f)
        //{
        //    Unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += itemData.StatsDatas.MaxHealth;
        //}
        Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().StatsData}");
        //}
        return true;
    }
    public bool AddInventoryItem(StatsItemSO itemObject)
    {
        if (_items.Count >= 8)
        {
            CheckItem = itemObject.GameObject();
            Debug.LogWarning("Inventory is full");

            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.SubUI.InventorySubUI.ChangeInventoryItem();
            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.UI, true);
            //아이템 교체하는 코드
            return false;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].item == itemObject)
            {
                Debug.Log($"Contians {itemObject.name}, fail add");
                return false;
            }
        }

        ////중복금지
        //if (items.Contains(itemObject))
        //{
        //    Debug.Log($"Contians {itemObject.name}, fail add");
        //    return false;
        //}
        //else
        //{
        Debug.Log($"Add {itemObject.name}, Success add {itemObject.name}");
        if (Unit.GetType() == typeof(Player))
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.AddItem(itemObject);
        ItemSet item = new ItemSet(itemObject, Time.time);
        _items.Add(item);

        ItemCount++;
        AddStat(itemObject.StatsDatas);
        //if (itemObject.StatsDatas.MaxHealth != 0.0f)
        //{
        //    Unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += itemObject.StatsDatas.MaxHealth;
        //}
        Debug.Log($"Change UnitStats {Unit.Core.GetCoreComponent<UnitStats>().StatsData}");
        //}
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

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].item == itemData)
            {
                Debug.Log($"Remove Item {itemData.name}");
                AddStat(itemData.StatsDatas * -1f);
                ////아이템에 추가 체력 증가 옵션이 있을 경우 현재 체력에서 아이템 추가 체력의 일정 비율 감소
                //if (itemData.StatsDatas.MaxHealth != 0.0f)
                //{
                //    Unit.Core.GetCoreComponent<UnitStats>().CurrentHealth -= itemData.StatsDatas.MaxHealth * 0.33f;
                //}

                _items.RemoveAt(i);

                if (unit.GetType() == typeof(Player))
                    GameManager.Inst.SubUI.InventorySubUI.InventoryItems.RemoveItem(itemData);

                //spawnItem
                GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, Unit.transform.position, GameManager.Inst.StageManager.IM.transform, itemData);
                break;
            }
        }

        //if (_items.Contains(itemData))
        //{

        //}
        //else
        //{
        //    Debug.Log($"Not Contians {itemData.name}, fail remove");
        //}
        return true;
    }
    public bool RemoveInventoryItem(ItemSet itemData)
    {
        if (itemData.item == null)
        {
            Debug.Log("Find not InventoryItem");
            return false;
        }

        if (_items.Contains(itemData))
        {
            Debug.Log($"Remove Item {itemData.item.name}");
            AddStat(itemData.item.StatsDatas * -1f);
            //if (itemData.item.StatsDatas.MaxHealth != 0.0f)
            //{
            //    Unit.Core.GetCoreComponent<UnitStats>().CurrentHealth -= itemData.item.StatsDatas.MaxHealth;
            //}

            _items.Remove(itemData);

            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.SubUI.InventorySubUI.InventoryItems.RemoveItem(itemData.item);

            //spawnItem
            GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, Unit.transform.position, GameManager.Inst.StageManager.IM.transform, itemData.item);

        }
        else
        {
            Debug.Log($"Not Contians {itemData.item.name}, fail remove");
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
    private void AddStat(StatsData statsData)
    {
        Unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData;
    }
}

using TGB.Item;
using TGB.Weapons;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Drawing;
using UnityEditor.Rendering;

[Serializable]
public class ItemSet
{
    public StatsItemSO item;
    public List<bool> init = new List<bool>();
    public List<float> startTime = new List<float>();
    public List<int> OnActionCount = new List<int>();
    public List<int> OnHitCount = new List<int>();
    public ItemSet(StatsItemSO itemSO, float _startTime = 0, int _actionCount = 0, int _hitCount = 0)
    {
        this.item = itemSO;

        if (this.startTime.Count < 1)
        {
            startTime.Add(_startTime);
        }

        if (this.OnActionCount.Count < 1)
        {
            OnActionCount.Add(_actionCount);
        }

        if (this.OnHitCount.Count < 1)
        {
            OnHitCount.Add(_hitCount);
        }
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

    public List<ItemSet> Items = new List<ItemSet>();
    public List<StatsItemSO> Inititems = new List<StatsItemSO>();
    public List<GameObject> InfinityEffectObjects = new List<GameObject>();
    public GameObject CheckItem;

    private Weapon m_weapon;
    private int ItemCount;
    private void Start()
    {
        weaponData = Weapon.weaponData;
        if (Items == null || Items?.Count == 0)
        {
            Debug.LogWarning($"{transform.name}'s Items is empty in The Inventory");
            Items = new List<ItemSet>();
        }

        if (Inititems.Count != 0)
        {
            foreach (var item in Inititems)
            {
                ItemSet _item = new ItemSet(item, Time.time);
                if (!Items.Contains(_item))
                {
                    Items.Add(_item);
                }
            }
        }

        ItemCount = Items.Count;
    }

    private void Update()
    {
        ItemExeUpdate(Unit);
    }
    public bool ItemOnInit(Unit unit)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].item.ItemEffects.Count; j++)
            {
                if (Items[i].item.ItemEffects[j] == null)
                    continue;

                if (Items[i].init.Count < j + 1)
                {
                    Items[i].init.Add(false);
                }
                Items[i].init[j] = Items[i].item.ExeInit(unit, Items[i].item.ItemEffects[j], Items[i].init[j]);
            }
        }
        return true;
    }

    /// <summary>
    /// 적중 시 효과
    /// </summary>
    /// <param name="unit">공격자</param>
    /// <returns></returns>
    public bool ItemOnHitExecute(Unit unit)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].item.ItemEffects.Count; j++)
            {
                if (Items[i].item.ItemEffects[j] == null)
                    continue;

                if (Items[i].OnHitCount.Count < j + 1)
                {
                    Items[i].OnHitCount.Add(0);
                }
                Items[i].OnHitCount[j] = Items[i].item.ExeOnHit(unit, Items[i].item.ItemEffects[j], Items[i].OnHitCount[j]);
            }
        }
        return true;
    }
    /// <summary>
    /// 적중 시 효과
    /// </summary>
    /// <param name="unit">공격 주체</param>
    /// <param name="Enemy">적중 당한 적</param>
    /// <returns></returns>
    public bool ItemOnHitExecute(Unit unit, Unit Enemy)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].item.ItemEffects.Count; j++)
            {
                if (Items[i].item.ItemEffects[j] == null)
                    continue;

                if (Items[i].OnHitCount.Count < j + 1)
                {
                    Items[i].OnHitCount.Add(0);
                }
                Items[i].OnHitCount[j] = Items[i].item.ExeOnHit(unit, Enemy, Items[i].item.ItemEffects[j], Items[i].OnHitCount[j]);
            }
        }
        return true;
    }

    /// <summary>
    /// 액션 시 효과
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public bool ItemActionExecute(Unit unit)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].item.ItemEffects.Count; j++)
            {
                if (Items[i].item.ItemEffects[j] == null)
                    continue;

                if (Items[i].OnActionCount.Count < j + 1)
                {
                    Items[i].OnActionCount.Add(0);
                }
                Items[i].OnActionCount[j] = Items[i].item.ExeAction(unit, Items[i].item.ItemEffects[j], Items[i].OnActionCount[j]);
            }
        }
        return true;
    }
    public bool ItemExeUpdate(Unit unit)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].item.ItemEffects.Count; j++)
            {
                if (Items[i].item.ItemEffects[j] == null)
                    continue;
                if (Items[i].startTime.Count < j + 1)
                {
                    Items[i].startTime.Add(0);

                }
                Items[i].startTime[j] = Items[i].item.ExeUpdate(unit, Items[i].item.ItemEffects[j], Items[i].startTime[j]);
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

    public bool AddInventoryItem(StatsItemSO itemObject, Transform EffectTransform = null)
    {
        if (Items.Count >= 8)
        {
            CheckItem = itemObject.GameObject();
            Debug.LogWarning("Inventory is full");

            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.SubUI.InventorySubUI.SetInventoryState(InventoryUI_State.Change);
            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.UI, true);
            //아이템 교체하는 코드
            return false;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].item == itemObject)
            {
                Debug.Log($"Contians {itemObject.name}, fail add");
                return false;
            }
        }

        //VFX
        if (itemObject.InitEffectData.AcquiredEffectPrefab != null)
            unit.Core.CoreEffectManager.StartEffects(itemObject.InitEffectData.AcquiredEffectPrefab, EffectTransform.position, Quaternion.identity);

        //InfinityVFX
        if (itemObject.InfinityEffectObjects.Count > 0 )
        {
            for (int i = 0; i < itemObject.InfinityEffectObjects.Count; i++)
            {
                var offset = new Vector3(itemObject.InfinityEffectObjects[i].EffectOffset.x * unit.Core.CoreMovement.FancingDirection, itemObject.InfinityEffectObjects[i].EffectOffset.y);
                var size = itemObject.InfinityEffectObjects[i].EffectScale;


                InfinityEffectObjects.Add(
                    itemObject.InfinityEffectObjects[i].isRandomPosRot ?
                    unit.Core.CoreEffectManager.StartEffectsWithRandomPosRot(
                        itemObject.InfinityEffectObjects[i].Object, itemObject.InfinityEffectObjects[i].isRandomRange, true,
                        size)
                    :
                    unit.Core.CoreEffectManager.StartEffects(
                        itemObject.InfinityEffectObjects[i].Object,
                        itemObject.InfinityEffectObjects[i].isGround ?
                        unit.Core.CoreCollisionSenses.GroundCenterPos + offset :
                        this.transform.position + offset, true,
                        size)
                    );
            }
        }

        //SFX
        if (itemObject.InitEffectData.AcquiredSoundEffect != null)
            unit.Core.CoreSoundEffect.AudioSpawn(itemObject.InitEffectData.AcquiredSoundEffect);

        Debug.Log($"Add {itemObject.name}, Success add {itemObject.name}");
        if (Unit.GetType() == typeof(Player))
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.AddItem(itemObject);
        ItemSet item = new ItemSet(itemObject, Time.time);
        Items.Add(item);

        ItemCount++;
        unit.Core.CoreUnitStats.AddStat(itemObject.StatsData);
        Debug.Log($"Change UnitStats {Unit.Core.CoreUnitStats.CalculStatsData}");
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

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].item == itemData)
            {
                Debug.Log($"Remove Item {itemData.name}");
                unit.Core.CoreUnitStats.AddStat(itemData.StatsData * -1f);

                Items.RemoveAt(i);

                //Destroy InfinityVFX
                if (itemData.InfinityEffectObjects.Count > 0)
                {
                    for (int j = 0; j < InfinityEffectObjects.Count; j++)
                    {
                        if (unit.Core.CoreEffectManager.ObjectPoolList.Contains(InfinityEffectObjects[j].GetComponent<EffectController>().parent))
                        {
                            var obj = InfinityEffectObjects[j];
                            InfinityEffectObjects.RemoveAt(j);
                            unit.Core.CoreEffectManager.ObjectPoolList.Remove(obj.GetComponent<EffectController>().parent);
                            Destroy(obj.GetComponent<EffectController>().parent.gameObject);
                        }
                    }
                }                

                if (unit.GetType() == typeof(Player))
                    GameManager.Inst.SubUI.InventorySubUI.InventoryItems.RemoveItem(itemData);

                //spawnItem
                GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, Unit.transform.position, GameManager.Inst.StageManager.IM.transform, itemData);
                break;
            }
        }
        return true;
    }
    public bool RemoveInventoryItem(ItemSet itemData)
    {
        if (itemData.item == null)
        {
            Debug.Log("Find not InventoryItem");
            return false;
        }

        if (Items.Contains(itemData))
        {
            Debug.Log($"Remove Item {itemData.item.name}");
            unit.Core.CoreUnitStats.AddStat(itemData.item.StatsData * -1f);

            Items.Remove(itemData);

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
}

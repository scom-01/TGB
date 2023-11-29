using System;
using System.Collections.Generic;
using TGB.Item;
using TGB.Weapons;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ItemSet
{
    public StatsItemSO item;
    public List<ItemEffectSet> itemEffectSets = new List<ItemEffectSet>();
    public List<bool> init = new List<bool>();
    public List<float> startTime = new List<float>();
    /// <summary>
    /// OnAction이면 OnAction의 Count를 OnHit면 OnHit의 Count를 계산
    /// </summary>
    public List<int> EffectCount = new List<int>();
    public ItemSet(StatsItemSO itemSO, ItemEffectSet _itemEffectSets = null)//  float _startTime = 0, int _Count = 0)
    {
        this.item = itemSO;

        if (_itemEffectSets == null)
        {
            itemEffectSets = new List<ItemEffectSet>();
            for (int i = 0; i < item.ItemEffects.Count; i++)
            {
                itemEffectSets.Add(new ItemEffectSet());
            }
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

    /// <summary>
    /// 현재 아이템
    /// </summary>
    public List<ItemSet> Items = new List<ItemSet>();

    /// <summary>
    /// 이전 아이템
    /// </summary>
    [HideInInspector] public List<ItemSet> Old_Items = new List<ItemSet>();

    /// <summary>
    /// 초기 보유 아이템
    /// </summary>
    public List<StatsItemSO> Inititems = new List<StatsItemSO>();

    /// <summary>
    /// 아이템 패시브 이펙트
    /// </summary>
    public List<GameObject> InfinityEffectObjects = new List<GameObject>();

    /// <summary>
    /// UI 현재 선택된 아이템
    /// </summary>
    public GameObject CheckItem;

    private Weapon m_weapon;
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
                ItemSet _item = new ItemSet(item);
                if (!Items.Contains(_item))
                {
                    Items.Add(_item);
                }
            }
        }
    }

    private void Update()
    {
        ItemExeUpdate(Unit);
    }

    #region 아이템 Event함수

    public bool ExeItemEffect(ITEM_TPYE type, Unit unit, Unit enemy = null)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            for (int j = 0; j < Items[i].itemEffectSets.Count; j++)
            {
                Items[i].itemEffectSets[j] = Items[i].item.ExeEffect(type, unit, enemy, Items[i].item.ItemEffects[j], Items[i].itemEffectSets[j]);
            }
        }
        return true;
    }

    /// <summary>
    /// 아이템의 Init Event 호출
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public bool ItemOnInit(ItemSet itemSet)
    {
        for (int i = 0; i < itemSet.itemEffectSets.Count; i++)
        {
            itemSet.itemEffectSets[i] = itemSet.item.ExeEffect(ITEM_TPYE.OnInit, unit, unit.TargetUnit, itemSet.item.ItemEffects[i], itemSet.itemEffectSets[i]);
        }
        return true;
    }

    /// <summary>
    /// 적중 시 효과
    /// </summary>
    /// <param name="unit">공격 주체</param>
    /// <param name="Enemy">적중 당한 적</param>
    /// <returns></returns>
    public bool ItemOnHitExecute(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnHit, unit, enemy);
        return true;
    }

    /// <summary>
    /// 액션 시 효과
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public bool ItemActionExecute(Unit unit)
    {
        ExeItemEffect(ITEM_TPYE.OnAction, unit, unit.TargetUnit);
        return true;
    }

    /// <summary>
    /// 업데이트 시 호출
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public bool ItemExeUpdate(Unit unit)
    {
        ExeItemEffect(ITEM_TPYE.OnUpdate, unit, unit.TargetUnit);
        return true;
    }

    /// <summary>
    /// 대쉬 시 호출
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="CanDash"></param>
    public void ItemExeDash(Unit unit, bool CanDash)
    {
        if (!CanDash)
            return;
        ExeItemEffect(ITEM_TPYE.OnDash, unit, unit.TargetUnit);
    }

    /// <summary>
    /// 씬 변경 시 호출
    /// </summary>
    /// <param name="unit"></param>
    public void ItemExeOnMoveMap(Unit unit)
    {
        ExeItemEffect(ITEM_TPYE.OnMoveMap, unit, unit.TargetUnit);
    }

    public void ItemExeOnDamage(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnDamaged, unit, enemy);
    }

    public void ItemExeOnCritical(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnCritical, unit, enemy);
    }

    public void ItemExeOnJump(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnJump, unit, enemy);
    }

    public void ItemExeOnLand(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnLand, unit, enemy);
    }

    public void ItemExeOnHealing(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnHealing, unit, enemy);
    }

    public void ItemExeOnDodge(Unit unit, Unit enemy = null)
    {
        ExeItemEffect(ITEM_TPYE.OnDodge, unit, enemy);
    }

    #endregion

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

    public bool AddInventoryItem(UnityEngine.Object Object)
    {
        StatsItemSO itemObject = new StatsItemSO();
        if (Object.GetType() == typeof(GameObject))
        {
            itemObject = Object.GameObject().GetComponent<SOB_Item>().Item;
        }
        else if (Object.GetType() == typeof(StatsItemSO))
        {
            itemObject = (StatsItemSO)Object;
        }

        if (Items.Count >= 8)
        {
            CheckItem = Object.GameObject();
            Debug.LogWarning("Inventory is full");

            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.SubUI.InventorySubUI.SetInventoryState(InventoryUI_State.Change);
            if (Unit.GetType() == typeof(Player))
                GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.UI, true);
            //아이템 교체하는 코드
            return false;
        }

        //조합 아이템 조합 여부
        if (CheckCompositeItem(Object))
        {
            return true;
        }

        //인벤토리 초과
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
            unit.Core.CoreEffectManager.StartEffects(itemObject.InitEffectData.AcquiredEffectPrefab, (Object.GameObject()?.transform == null) ? this.transform.position : Object.GameObject().transform.position, Quaternion.identity, Vector3.one);

        //InfinityVFX
        if (itemObject.InfinityEffectObjects.Count > 0)
        {
            for (int i = 0; i < itemObject.InfinityEffectObjects.Count; i++)
            {
                var offset = new Vector3(itemObject.InfinityEffectObjects[i].EffectOffset.x * unit.Core.CoreMovement.FancingDirection, itemObject.InfinityEffectObjects[i].EffectOffset.y);
                var size = itemObject.InfinityEffectObjects[i].EffectScale;

                if (itemObject.InfinityEffectObjects[i].Object == null)
                    continue;

                InfinityEffectObjects.Add(
                    itemObject.InfinityEffectObjects[i].isRandomPosRot ?
                    unit.Core.CoreEffectManager.StartEffectsWithRandomPosRot(
                        itemObject.InfinityEffectObjects[i].Object, itemObject.InfinityEffectObjects[i].isRandomRange, size, true)
                    :
                    unit.Core.CoreEffectManager.StartEffectsPos(
                        itemObject.InfinityEffectObjects[i].Object,
                        itemObject.InfinityEffectObjects[i].isGround ?
                        unit.Core.CoreCollisionSenses.GroundCenterPos + offset :
                        this.transform.position + offset, size, true)
                    );
            }
        }

        //SFX
        if (itemObject.InitEffectData.AcquiredSFX.Clip != null)
            unit.Core.CoreSoundEffect.AudioSpawn(itemObject.InitEffectData.AcquiredSFX);

        //미해금 아이템이라면 미해금 아이템 리스트에 추가
        if (DataManager.Inst.JSON_DataParsing.LockItemList.Contains(itemObject.ItemIdx))
        {
            DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.WaitUnlockItemIdxs.Add(itemObject.ItemIdx);
        }

        Debug.Log($"Add {itemObject.name}, Success add {itemObject.name}");
        if (Unit.GetType() == typeof(Player))
            GameManager.Inst.SubUI.InventorySubUI.InventoryItems.AddItem(itemObject);

        //Old_Items 리스트에 itemObject이 존재 한다면 return ItemSet;
        ItemSet item = ContainsItem(Old_Items, itemObject);
        if (item == null)
        {
            item = new ItemSet(itemObject);
        }

        Items.Add(item);
        ItemOnInit(item);

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

                //한 번 획득 한 아이템의 정보를 저장 후 재 획득 시 정보를 덮어씌움
                if (!Old_Items.Contains(Items[i]))
                    Old_Items.Add(Items[i]);

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
                GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, Unit.Core.CoreCollisionSenses.UnitCenterPos, GameManager.Inst.StageManager.IM.transform, itemData);
                break;
            }
        }
        return true;
    }

    private ItemSet ContainsItem(List<ItemSet> itemSets, StatsItemSO item)
    {
        for (int i = 0; i < itemSets.Count; i++)
        {
            if (itemSets[i].item == item)
                return itemSets[i];
        }
        return null;
    }

    /// <summary>
    /// 조합 아이템 여부
    /// true 시 AddInventoryItem(itemObject)을 재호출 해야함
    /// </summary>
    /// <param name="Object"></param>
    /// <returns></returns>
    private bool CheckCompositeItem(UnityEngine.Object Object)
    {
        StatsItemSO itemObject = new StatsItemSO();
        if (Object.GetType() == typeof(GameObject))
        {
            itemObject = Object.GameObject().GetComponent<SOB_Item>().Item;
        }
        else if (Object.GetType() == typeof(StatsItemSO))
        {
            itemObject = (StatsItemSO)Object;
        }

        //조합 아이템 조합 여부
        if (itemObject.CompositeItems.Count == 0)
            return false;

        for (int i = 0; i < itemObject.CompositeItems.Count; i++)
        {
            for (int j = 0; j < Items.Count; j++)
            {
                if (itemObject.CompositeItems[i].MaterialItem != Items[j].item)
                    continue;
                //재료 아이템 제거(인벤토리)
                if (unit.GetType() == typeof(Player))
                    GameManager.Inst.SubUI.InventorySubUI.InventoryItems.RemoveItem(Items[j].item);

                //한 번 획득 한 아이템의 정보를 저장 후 재 획득 시 정보를 덮어씌움
                if (!Old_Items.Contains(Items[i]))
                    Old_Items.Add(Items[i]);

                Items.RemoveAt(j);

                //합성 VFX
                if (itemObject.CompositeItems[i].EditVFX != null)
                    unit.Core.CoreEffectManager.StartEffects(itemObject.CompositeItems[i].EditVFX, (Object.GameObject()?.transform == null) ? this.transform.position : Object.GameObject().transform.position, Quaternion.identity, Vector3.one);

                //합성 SFX
                if (itemObject.CompositeItems[i].EditSFX.Clip != null)
                    unit.Core.CoreSoundEffect.AudioSpawn(itemObject.CompositeItems[i].EditSFX);

                itemObject = itemObject.CompositeItems[i].ResultItem;
                if (Object.GameObject() != null)
                {
                    Object.GameObject().GetComponent<SOB_Item>().Item = itemObject;
                    Object.GameObject().GetComponent<SOB_Item>().Init();
                }
                return AddInventoryItem(itemObject);
            }

        }
        return false;
    }
}

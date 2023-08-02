using SOB.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine.Localization;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Stats Data")]
public class StatsItemSO : ItemDataSO
{
    [Tooltip("아이템이 갖는 스탯")]
    public StatsData StatsData;
    [Tooltip("최대체력이 아닌 현재 체력 증가값")]
    public int Health;
    public EffectData effectData;

    [field: SerializeField] public List<ItemEffectSO> ItemEffects = new List<ItemEffectSO>();
    private List<ItemEffectSO> clone = new List<ItemEffectSO>();

    private List<ItemEffectSO> itemEffects
    {
        get
        {
            if (clone.Count == ItemEffects.Count)
            {
                return clone;
            }
            clone.Clear();
            for (int i = 0; i < ItemEffects.Count; i++)
            {
                var str = ItemEffects[i].name;
                clone.Add(Instantiate(ItemEffects[i]) as ItemEffectSO);
                clone[clone.Count - 1].name = str;
            }
            return clone;
        }
    }

    public virtual int ExeAction(Unit unit, ItemEffectSO _itemEffect, int attackCount = 0)
    {
        return attackCount = _itemEffect.ExecuteOnAction(this, unit, attackCount);
    }

    public virtual int ExeOnHit(Unit unit, ItemEffectSO itemEffect ,int attackCount = 0)
    {
        return attackCount = itemEffect.ExecuteOnHit(this,unit, attackCount);
    }
    /// <summary>
    /// 적중 시 효과
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="Enemy">적중당한 적</param>
    /// <param name="itemEffect"></param>
    /// <param name="attackCount">적중 횟수</param>
    /// <returns></returns>
    public virtual int ExeOnHit(Unit unit, Unit Enemy, ItemEffectSO itemEffect, int attackCount = 0)
    {
        return attackCount = itemEffect.ExecuteOnHit(this, unit, Enemy, attackCount);
    }
    public virtual float ExeUpdate(Unit unit, float startTime = 0)
    {
        for (int i = 0; i < itemEffects.Count; i++)
        {
            itemEffects[i].ContinouseEffectExcute(this, unit, startTime);            
        }
        return startTime;
    }
    /// <summary>
    /// 특정 시간마다 효과 부여
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="itemEffect"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    public virtual float ExeUpdate(Unit unit, ItemEffectSO itemEffect, float startTime = 0)
    {
        return startTime = itemEffect.ContinouseEffectExcute(this, unit, startTime);
    }
}

[Serializable]
public struct EffectData
{
    [field: Header("Collider Use")]
    [field: Tooltip("획득 시 이펙트")]
    [field: SerializeField] public GameObject AcquiredEffectPrefab { get; private set; }
    [field: Tooltip("획득 시 사운드이펙트")]
    [field: SerializeField] public AudioClip AcquiredSoundEffect { get; private set; }

    [field: Tooltip("아이템 소비 여부")]
    [field: SerializeField] public bool isEquipment { get; private set; }

    //--Detect--
    [field: Header("Detect Use")]
    [field: Tooltip("Detect 시 SubUI 표시 여부")]
    [field: SerializeField] public bool DetailSubUI { get; private set; }
}
using System;
using System.Collections.Generic;
using TGB.Weapons.Components;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using static UnityEngine.EventSystems.EventTrigger;

[Serializable]
public struct Write_StatsData_item
{
    public Stats_TYPE type;
    public LocalizedString StatsLocalizeString;
    [Tooltip("부가 조건")]
    public float variable;
    [Tooltip("아이템 효과의 값")]
    public float value;
    [Tooltip("퍼센트 표기 여부")]
    public bool ShowPercent;
    [Tooltip("부호 표기 여부(+,-)")]
    public bool HideMark;
}

[Serializable]
public struct ItemComposite
{
    public StatsItemSO MaterialItem;
    public StatsItemSO ResultItem;
    public AudioClip EditSFX;
    public GameObject EditVFX;
}

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Stats Data")]
public class StatsItemSO : ItemDataSO
{
    public List<ItemComposite> CompositeItems;
    /// <summary>
    /// 필드 드랍
    /// </summary>
    public bool isFieldSpawn;

    [Header("--StatsData--")]
    [Tooltip("아이템이 갖는 스탯")]
    public StatsData StatsData = new StatsData();

    [Tooltip("표기되는 아이템 효과")]
    public List<Write_StatsData_item> StatsItems = new List<Write_StatsData_item>();
    
    /// <summary>
    /// 아이템의 스탯 설명
    /// </summary>
    public string StatsData_Descripts
    {
        get
        {
            string temp = "";
            for (int i = 0; i < StatsItems.Count; i++)
            {
                temp += (LocalizationSettings.StringDatabase.GetTableEntry("Stats_Table", (StatsItems[i].StatsLocalizeString.TableEntryReference.KeyId == 0) ?
                    StatsItems[i].type.ToString() :
                    StatsItems[i].StatsLocalizeString.TableEntryReference).Entry.Value)
                    + (StatsItems[i].HideMark ? " " : ((StatsItems[i].value >= 0) ? (" +") : " -")) + Mathf.Abs(StatsItems[i].value);
                string var = "{variable}";
                if (temp.Contains(var))
                {
                    temp = temp.Replace(var, StatsItems[i].variable.ToString());
                }   

                if (StatsItems[i].ShowPercent)
                {
                    temp += "%";
                }

                temp += "\n";
            }
            return temp;
        }
    }
    [Tooltip("최대체력이 아닌 현재 체력 증가값")]
    public int Health;

    [Header("--Buff--")]
    public List<BuffItemSO> buffItems = new List<BuffItemSO>();
    [Header("--Effects--")]
    public EffectData InitEffectData;
    public List<EffectPrefab> InfinityEffectObjects = new List<EffectPrefab>();
    [Header("--ItemEffects--")]
    [field: SerializeField] public List<ItemEffectSO> ItemEffects = new List<ItemEffectSO>();

    #region ExeInit
    public virtual bool ExeInit(Unit unit, Unit enemy, ItemEffectSO _itemEffect, bool isInit)
    {
        return isInit = _itemEffect.ExecuteOnInit(this, unit, enemy, isInit);
    }
    public virtual bool ExeInit(Unit unit, ItemEffectSO _itemEffect, bool isInit)
    {
        return ExeInit(unit, null, _itemEffect, isInit);
    }
    #endregion

    #region ExeAction
    
    /// <summary>
    /// Action 시 효과
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="_itemEffect"></param>
    /// <param name="attackCount"></param>
    /// <returns></returns>
    public virtual int ExeAction(Unit unit, Unit enemy, ItemEffectSO _itemEffect, int attackCount = 0)
    {
        return attackCount = _itemEffect.ExecuteOnAction(this, unit, enemy, attackCount);
    }

    public virtual int ExeAction(Unit unit, ItemEffectSO _itemEffect, int attackCount = 0)
    {
        return ExeAction(unit, null, _itemEffect, attackCount);
    }
    #endregion

    #region ExeOnHit
    /// <summary>
    /// 적중 시 효과
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="enemy">적중당한 적</param>
    /// <param name="itemEffect"></param>
    /// <param name="attackCount">적중 횟수</param>
    /// <returns></returns>
    public virtual int ExeOnHit(Unit unit, Unit enemy, ItemEffectSO itemEffect, int attackCount = 0)
    {
        return attackCount = itemEffect.ExecuteOnHit(this, unit, enemy, attackCount);
    }
    public virtual int ExeOnHit(Unit unit, ItemEffectSO itemEffect ,int attackCount = 0)
    {
        return ExeOnHit(unit, null, itemEffect, attackCount);
    }
    #endregion

    #region ExeUpdate
    /// <summary>
    /// 특정 시간마다 효과 부여
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="itemEffect"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    public virtual float ExeUpdate(Unit unit, Unit enemy, ItemEffectSO itemEffect, float startTime = 0)
    {
        return startTime = itemEffect.ContinouseEffectExcute(this, unit, enemy, startTime);
    }
    public virtual float ExeUpdate(Unit unit, ItemEffectSO itemEffect, float startTime = 0)
    {
        return ExeUpdate(unit, null, itemEffect, startTime);
    }
    #endregion

    #region ExeDash
    /// <summary>
    /// 대쉬 시 효과 부여
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="itemEffect"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    public virtual bool ExeDash(Unit unit, Unit enemy, ItemEffectSO itemEffect, bool CanDash)
    {
        return CanDash = itemEffect.ExcuteOnDash(this, unit, enemy, CanDash);
    }
    public virtual bool ExeDash(Unit unit, ItemEffectSO itemEffect, bool CanDash)
    {
        return ExeDash(unit, null, itemEffect, CanDash);
    }
    #endregion

    #region ExeOnChangeScene
    /// <summary>
    /// 씬 변경 시 호출
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="itemEffect"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    public virtual void ExeMoveMap(Unit unit, Unit enemy, ItemEffectSO itemEffect)
    {
        itemEffect.ExcuteOnMoveMap(this, unit, enemy);
    }
    public virtual void ExeMoveMap(Unit unit, ItemEffectSO itemEffect)
    {
        ExeMoveMap(unit, null, itemEffect);
    }
    #endregion
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

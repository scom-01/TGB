using System;
using System.Net.Http.Headers;
using TGB;
using TGB.Weapons.Components;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class ItemEffectSet
{
    /// <summary>
    /// 아이템 획득 시 초기 1회
    /// </summary>
    public bool init = false;
    /// <summary>
    /// 호출 시 시간
    /// </summary>
    public float startTime = 0;
    /// <summary>
    /// 횟수 계산용 변수(OnAction or OnHit etc.)
    /// </summary>
    public int Count = 0;
    public ItemEffectSet(bool init = false, float startTime = 0, int count = 0)
    {
        this.init = init;
        this.startTime = startTime;
        Count = count;
    }
}

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemEffect Data")]
public abstract class ItemEffectSO : ScriptableObject, IExecuteEffect
{
    public ITEM_TPYE Item_Type;
    public ItemEffectData itemEffectData;
    [field: Header("Effect")]
    public Sprite EffectSprite;
    [field: Tooltip("아이템 효과 설명")]
    [field: SerializeField] public LocalizedString EffectDescriptionLocal { get; private set; }
    /// <summary>
    /// Effect 효과 시 생성될 VFX
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public GameObject SpawnVFX(Unit unit)
    {
        if (unit == null)
            return null;

        if (itemEffectData.VFX.Object == null)
            return null;
        GameObject go = itemEffectData.VFX.SpawnObject(unit);
        return go;
    }

    /// <summary>
    /// Effect 효과 시 생성될 SFX
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public bool SpawnSFX(Unit unit)
    {
        if (unit == null)
            return false;

        if (itemEffectData.SFX.Clip == null)
            return false;

        unit.Core.CoreSoundEffect.AudioSpawn(itemEffectData.SFX);

        return true;
    }

    public virtual ItemEffectSet ExcuteEffect(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEffectSet itemEffectSet)
    {
        if (Item_Type != type || Item_Type == ITEM_TPYE.None || itemEffectSet == null)
            return itemEffectSet;

        return itemEffectSet;
    }
}

[Serializable]
public struct ItemEffectData
{
    /// <summary>
    /// 필요 호출 회수
    /// </summary>
    [Range(1, 99)]
    public int MaxCount;
    /// <summary>
    /// 재사용 대기시간
    /// </summary>
    public float CooldownTime;
    [HideInInspector] public bool isExecute;
    /// <summary>
    /// Effect VFX
    /// </summary>
    public EffectPrefab VFX;
    /// <summary>
    /// Effect SFX
    /// </summary>
    public AudioPrefab SFX;
}
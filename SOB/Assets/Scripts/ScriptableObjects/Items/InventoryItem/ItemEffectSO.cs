using SOB.Manager;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemEffect Data")]
public abstract class ItemEffectSO : ScriptableObject, IExecuteEffect, IExecuteContinuousEffect
{
    public ItemEffectData itemEffectData;
    public virtual int ExecuteEffect(StatsItemSO parentItem, Unit unit, int attackCount)
    {
        return attackCount;
    }
    public virtual int ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit Enemy,int attackCount)
    {
        return attackCount;
    }
    public virtual float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, float startTime)
    {
        return  startTime;
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
    public GameObject VFX;
}
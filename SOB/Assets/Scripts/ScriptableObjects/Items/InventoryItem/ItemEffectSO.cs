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
    [Range(1, 99)]
    public int MaxCount;
    public float durationTime;
    [HideInInspector] public bool isExecute;
    /// <summary>
    /// Effect VFX
    /// </summary>
    public GameObject VFX;
}
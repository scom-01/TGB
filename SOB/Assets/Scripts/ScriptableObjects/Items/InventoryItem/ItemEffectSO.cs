using SOB.Manager;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemEffect Data")]
public abstract class ItemEffectSO : ScriptableObject, IExecuteEffect, IExecuteContinuousEffect
{
    [HideInInspector] public int AttackCount = 0;
    [Range(1, 99)]
    public int MaxCount;
    [HideInInspector] public float startTime;
    public float durationTime;
    [HideInInspector] public bool isExecute = false;
    /// <summary>
    /// Effect VFX
    /// </summary>
    public GameObject VFX;

    public virtual void ExecuteEffect(StatsItemSO parentItem, Unit unit)
    {
        
    }
    public virtual void ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit Enemy)
    {

    }
    public virtual void ContinouseEffectExcute(StatsItemSO parentItem, Unit unit)
    {
    }

}
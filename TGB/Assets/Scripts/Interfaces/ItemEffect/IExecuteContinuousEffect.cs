using UnityEngine;


public interface IExecuteContinuousEffect
{
    public float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, Unit enemy, float startTime);
}

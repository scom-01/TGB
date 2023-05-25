using UnityEngine;


public interface IExecuteEffect
{
    public int ExecuteEffect(StatsItemSO parentItem, Unit unit, int attackCount);
    public int ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount);
}

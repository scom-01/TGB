using UnityEngine;


public interface IExecuteEffect
{
    public int ExecuteAction(StatsItemSO parentItem, Unit unit, int attackCount);
    public int ExecuteOnHit(StatsItemSO parentItem, Unit unit, int attackCount);
    public int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount);
}

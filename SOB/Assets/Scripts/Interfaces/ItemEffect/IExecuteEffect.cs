using UnityEngine;


public interface IExecuteEffect
{
    public bool ExecuteOnInit(StatsItemSO parentItem, Unit unit, Unit enemy, bool isInit);
    public int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount);
    public int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount);
}

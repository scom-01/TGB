using UnityEngine;


public interface IExecuteEffect
{
    public void ExecuteEffect(StatsItemSO parentItem, Unit unit);
    public void ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit enemy);
}

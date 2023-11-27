using UnityEngine;


public interface IExecuteEffect
{
    public ItemEffectSet ExcuteEffect(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEffectSet itemEffectSet);
}

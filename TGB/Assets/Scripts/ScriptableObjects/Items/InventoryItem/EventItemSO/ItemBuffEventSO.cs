using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEventSO
{
    [Header("Buff Event")]
    public List<BuffItemSO> buffItems = new List<BuffItemSO>();
    [SerializeField]
    private bool isSelf;

    private void BuffEvent(Unit unit)
    {
        if (unit == null)
            return;

        if (buffItems == null)
            return;

        for (int i = 0; i < buffItems.Count; i++)
        {
            if (Buff.BuffSystemAddBuff(unit, buffItems[i]) == null)
                return;
        }

        SpawnVFX(unit);
        SpawnSFX(unit);
    }

    public override ItemEventSet ExcuteEvent(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEventSet itemEventSet)
    {
        if (Item_Type != type || Item_Type == ITEM_TPYE.None || itemEventSet == null)
            return itemEventSet;

        if (!itemEventSet.init)
        {
            itemEventSet.init = true;
        }

        if (GameManager.Inst.PlayTime < itemEventSet.startTime + itemEventData.CooldownTime)
        {
            Debug.Log($"itemEffectSet.CoolTime = {GameManager.Inst.PlayTime - itemEventSet.startTime}");
            return itemEventSet;
        }

        itemEventSet.Count++;
        if (itemEventSet.Count >= itemEventData.MaxCount && itemEventData.Percent >= Random.Range(0f, 100f))
        {
            if (isSelf)
            {
                BuffEvent(unit);
            }
            else
            {
                if (enemy == null)
                {
                    itemEventSet.Count--;
                    return itemEventSet;
                }
                BuffEvent(enemy);
            }
            itemEventSet.Count = 0;
            itemEventSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEventSet;
    }
}

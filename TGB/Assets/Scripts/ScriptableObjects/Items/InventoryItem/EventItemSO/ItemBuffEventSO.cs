using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
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

    public override ItemEffectSet ExcuteEffect(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEffectSet itemEffectSet)
    {
        if (Item_Type != type || Item_Type == ITEM_TPYE.None || itemEffectSet == null)
            return itemEffectSet;

        if (!itemEffectSet.init)
        {
            itemEffectSet.init = true;
        }

        if (GameManager.Inst.PlayTime < itemEffectSet.startTime + itemEffectData.CooldownTime)
        {
            Debug.Log($"itemEffectSet.CoolTime = {GameManager.Inst.PlayTime - itemEffectSet.startTime}");
            return itemEffectSet;
        }

        itemEffectSet.Count++;
        if (itemEffectSet.Count >= itemEffectData.MaxCount && itemEffectData.Percent >= Random.Range(0f, 100f))
        {
            if (isSelf)
            {
                BuffEvent(unit);
            }
            else
            {
                BuffEvent(enemy);
            }
            itemEffectSet.Count = 0;
            itemEffectSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEffectSet;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
{
    [Header("Buff Event")]
    public BuffItemSO buffItem;

    private void BuffEvent(Unit unit)
    {
        if (buffItem == null)
            return;

        if (Buff.BuffSystemAddBuff(unit, buffItem) == null)
            return;

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
        if (itemEffectSet.Count >= itemEffectData.MaxCount)
        {
            if (enemy != null)
            {
                BuffEvent(enemy);
            }
            else
            {
                BuffEvent(unit);
            }
            itemEffectSet.Count = 0;
            itemEffectSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEffectSet;
    }
}

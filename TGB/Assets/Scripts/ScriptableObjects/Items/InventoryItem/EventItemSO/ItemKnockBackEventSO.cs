using UnityEngine;

[CreateAssetMenu(fileName = "newKnockBackEvent", menuName = "Data/Item Data/ItemKnockBackEventSO Data")]
public class ItemKnockBackEventSO : ItemEventSO
{
    [Header("KnockBack Event")]
    public Vector2 angle;
    public float strength;
    
    private void KnockBackAction(Unit unit, Unit enemy = null)
    {
        if (enemy == null)
            return;

        SpawnVFX(unit);
        SpawnSFX(unit);

        enemy.Core.CoreKnockBackReceiver.KnockBack(angle, strength, unit.Core.CoreMovement.FancingDirection);
    }

    public override ItemEventSet ExcuteEvent(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEventSet itemEffectSet)
    {
        if (Item_Type != type || Item_Type == ITEM_TPYE.None || itemEffectSet == null)
            return itemEffectSet;

        if (!itemEffectSet.init)
        {
            itemEffectSet.init = true;
        }

        if (GameManager.Inst.PlayTime < itemEffectSet.startTime + itemEventData.CooldownTime)
        {
            Debug.Log($"itemEffectSet.CoolTime = {GameManager.Inst.PlayTime - itemEffectSet.startTime}");
            return itemEffectSet;
        }

        itemEffectSet.Count++;
        if (itemEffectSet.Count >= itemEventData.MaxCount && itemEventData.Percent >= Random.Range(0f, 100f))
        {
            KnockBackAction(unit, enemy);
            itemEffectSet.Count = 0;
            itemEffectSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEffectSet;
    }    
}

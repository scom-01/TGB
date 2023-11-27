using UnityEngine;

[CreateAssetMenu(fileName = "newAttackEventData", menuName = "Data/Item Data/ItemAttackEventSO Data")]
public class ItemAttackEventSO : ItemEffectSO
{
    [Header("Attack Event")]
    public int AdditionalDamage;
    /// <summary>
    /// 고정 데미지 or 스탯 반영 데미지
    /// </summary>
    public bool isFixed;

    /// <summary>
    /// 스스로에게 피해
    /// </summary>
    public bool isSelf_harm;

    /// <summary>
    /// 피해량 흡혈
    /// </summary>
    public bool isBloodsucking;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit">피해자</param>
    /// <param name="enemy">가해자</param>
    private void AttackAction(StatsItemSO parentItem, Unit unit, Unit enemy = null)
    {
        //스스로에게 피해
        if (isSelf_harm)
        {
            SpawnVFX(unit);
            SpawnSFX(unit);

            if (isFixed)
            {
                //고정데미지
                unit.Core.CoreDamageReceiver.FixedDamage(AdditionalDamage, true);
            }
            else
            {
                unit.Core.CoreDamageReceiver.TrueDamage(
                    unit,
                    parentItem.StatsData.Elemental,
                    parentItem.StatsData.DamageAttiribute,
                    unit.Core.CoreUnitStats.CalculStatsData.DefaultPower + AdditionalDamage
                    );
            }
        }
        else
        {
            if (enemy == null)
                return;

            SpawnVFX(enemy);
            SpawnSFX(enemy);

            if (isFixed)
            {
                //고정데미지
                enemy.Core.CoreDamageReceiver.FixedDamage(AdditionalDamage, true);
                if (isBloodsucking)
                    unit.Core.CoreUnitStats.IncreaseHealth(AdditionalDamage);
            }
            else
            {
                enemy.Core.CoreDamageReceiver.TrueDamage(
                    unit,
                    parentItem.StatsData.Elemental,
                    parentItem.StatsData.DamageAttiribute,
                    unit.Core.CoreUnitStats.CalculStatsData.DefaultPower + AdditionalDamage
                    );
                if (isBloodsucking)
                    unit.Core.CoreUnitStats.IncreaseHealth(unit.Core.CoreUnitStats.CalculStatsData.DefaultPower + AdditionalDamage);
            }
        }
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
            AttackAction(parentItem, unit, enemy);
            itemEffectSet.Count = 0;
            itemEffectSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEffectSet;
    }
}

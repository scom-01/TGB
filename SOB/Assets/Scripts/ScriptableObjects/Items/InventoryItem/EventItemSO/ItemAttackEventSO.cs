using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
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
    public bool isSelf_harm;
    public bool isBloodsucking;
    private void AttackAction(StatsItemSO parentItem, Unit unit, Unit enemy = null)
    {
        //스스로에게 피해
        if(isSelf_harm)
        {
            if (itemEffectData.VFX != null)
                unit.Core.CoreEffectManager.StartEffects(itemEffectData.VFX, unit.Core.CoreCollisionSenses.GroundCheck.position);

            if (isFixed)
            {
                //고정데미지
                unit.Core.CoreDamageReceiver.FixedDamage(AdditionalDamage, true);
            }
            else
            {
                unit.Core.CoreDamageReceiver.TrueDamage(
                    unit.Core.CoreUnitStats.StatsData,
                    unit.Core.CoreUnitStats.StatsData,
                    parentItem.StatsData.Elemental,
                    parentItem.StatsData.DamageAttiribute,
                    unit.Core.CoreUnitStats.StatsData.DefaultPower + AdditionalDamage
                    );
            }
        }
        else
        {
            if (enemy == null)
                return;

            if (itemEffectData.VFX != null)
                unit.Core.CoreEffectManager.StartEffects(itemEffectData.VFX, unit.Core.CoreCollisionSenses.GroundCheck.position);


            if (isFixed)
            {
                //고정데미지
                enemy.Core.CoreDamageReceiver.FixedDamage(AdditionalDamage, true);
                if(isBloodsucking)
                    unit.Core.CoreUnitStats.IncreaseHealth(AdditionalDamage);
            }
            else
            {
                enemy.Core.CoreDamageReceiver.TrueDamage(
                    unit.Core.CoreUnitStats.StatsData,
                    enemy.Core.CoreUnitStats.StatsData,
                    parentItem.StatsData.Elemental,
                    parentItem.StatsData.DamageAttiribute,
                    unit.Core.CoreUnitStats.StatsData.DefaultPower + AdditionalDamage
                    );
                if (isBloodsucking)
                    unit.Core.CoreUnitStats.IncreaseHealth(unit.Core.CoreUnitStats.StatsData.DefaultPower + AdditionalDamage);
            }
        }
    }

    public override int ExecuteOnAction(StatsItemSO parentItem, Unit unit, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");

        if (attackCount >= itemEffectData.MaxCount)
        {            
            AttackAction(parentItem, unit);
            attackCount = 0;
        }
        return attackCount;
    }

    public override int ExecuteOnHit(StatsItemSO parentItem, Unit unit, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnHit || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if (attackCount >= itemEffectData.MaxCount)
        {
            AttackAction(parentItem,unit);
            attackCount = 0;
        }
        return attackCount;
    }
    public override int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnHit || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if (attackCount >= itemEffectData.MaxCount)
        {
            AttackAction(parentItem,unit,enemy);
            attackCount = 0;
        }
        return attackCount;
    }

    public override float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, float startTime)
    {
        if (Item_Type != ITEM_TPYE.OnUpdate || Item_Type == ITEM_TPYE.None)
            return startTime;

        if (GameManager.Inst.PlayTime >= startTime + itemEffectData.CooldownTime)
        {
            AttackAction(parentItem, unit);
            startTime = GameManager.Inst.PlayTime;
        }
        return startTime;
    }
}

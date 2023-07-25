using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemAttackEvent Data")]
public class ItemAttackEventSO : ItemEffectSO
{
    public int additionalDamage;
    public override int ExecuteEffect(StatsItemSO parentItem, Unit unit, int attackCount)
    {
        attackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if(attackCount >= itemEffectData.MaxCount)
        {
            if(itemEffectData.VFX != null)
                unit.Core.GetCoreComponent<EffectManager>().StartEffects(itemEffectData.VFX, unit.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);
            attackCount = 0;
        }
        return attackCount;
    }
    public override int ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        attackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if(attackCount >= itemEffectData.MaxCount)
        {
            if(itemEffectData.VFX != null)
                unit.Core.GetCoreComponent<EffectManager>().StartEffects(itemEffectData.VFX, enemy.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);

            //공격자스탯(기본 스탯 + 아이템들의 스탯) + 아이템의 속성
            enemy.Core.GetCoreComponent<DamageReceiver>().Damage(unit.Core.GetCoreComponent<UnitStats>().StatsData,
                    enemy.Core.GetCoreComponent<UnitStats>().StatsData,
                    parentItem.StatsDatas.Elemental,
                    parentItem.StatsDatas.DefaultPower + additionalDamage);
            attackCount = 0;
        }
        return attackCount;
    }
}

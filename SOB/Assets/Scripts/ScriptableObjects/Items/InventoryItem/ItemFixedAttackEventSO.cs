using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFixedDamageEventData", menuName = "Data/Item Data/ItemFixedAttackEventSO Data")]
public class ItemFixedAttackEventSO : ItemEffectSO
{
    public int FixedDamage;
    public override int ExecuteOnHit(StatsItemSO parentItem, Unit unit, int attackCount)
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
    public override int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        attackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if(attackCount >= itemEffectData.MaxCount)
        {
            if(itemEffectData.VFX != null)
                unit.Core.GetCoreComponent<EffectManager>().StartEffects(itemEffectData.VFX, enemy.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);

            //고정데미지
            enemy.Core.GetCoreComponent<DamageReceiver>().FixedDamage(FixedDamage, true);
            attackCount = 0;
        }
        return attackCount;
    }
}

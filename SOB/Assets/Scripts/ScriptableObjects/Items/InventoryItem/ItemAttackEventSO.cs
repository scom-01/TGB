using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemAttackEvent Data")]
public class ItemAttackEventSO : ItemEffectSO
{
    public BuffItemSO buffItem;

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
            attackCount = 0;
        }
        return attackCount;
    }
}

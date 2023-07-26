using SOB.CoreSystem;
using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileAttackEventData", menuName = "Data/Item Data/ItemProjectileAttackEventSO Data")]
public class ItemProjectileAttackEventSO : ItemEffectSO
{
    public ProjectileData ProjectileData;
    public override int ExecuteAction(StatsItemSO parentItem, Unit unit, int attackCount)
    {
        attackCount++;
        Debug.Log("ExcuteEffect Attack!");

        if(attackCount >= itemEffectData.MaxCount)
        {
            if(itemEffectData.VFX != null)
                unit.Core.GetCoreComponent<EffectManager>().StartEffects(itemEffectData.VFX, unit.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);

            unit.Core.GetCoreComponent<EffectManager>().StartProjectileCheck(unit, ProjectileData);
            attackCount = 0;
        }
        return attackCount;
    }
}

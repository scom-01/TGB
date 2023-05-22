using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemAttackEvent Data")]
public class ItemAttackEventSO : ItemEffectSO
{
    private int AttackCount = 0;
    public BuffItemSO buffItem;

    private float startTime;
    public override void ExecuteEffect(StatsItemSO parentItem, Unit unit)
    {
        AttackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if(AttackCount >= MaxCount)
        {
            if(VFX != null)
                unit.Core.GetCoreComponent<EffectManager>().StartEffects(VFX, unit.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);
            AttackCount = 0;
        }
    }
    public override void ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit enemy)
    {
        AttackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if(AttackCount >= MaxCount)
        {
            if(VFX != null)
                unit.Core.GetCoreComponent<EffectManager>().StartEffects(VFX, enemy.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);
            AttackCount = 0;
        }
    }
}

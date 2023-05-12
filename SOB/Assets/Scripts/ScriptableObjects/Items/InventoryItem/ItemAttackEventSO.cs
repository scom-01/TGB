using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemAttackEvent Data")]
public class ItemAttackEventSO : ItemEffectSO
{
    public int AttackCount = 0;
    [Range(1, 9)]
    public int MaxCount;

    public GameObject VFX;
    public BuffItemSO buffItem;
    public override void ExcuteEffect(StatsItemSO parentItem, Unit unit)
    {
        AttackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if(AttackCount >= MaxCount)
        {
            if(VFX != null)
                unit.Core.GetCoreComponent<ParticleManager>().StartParticles(VFX, unit.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);
            AttackCount = 0;
        }
    }
}

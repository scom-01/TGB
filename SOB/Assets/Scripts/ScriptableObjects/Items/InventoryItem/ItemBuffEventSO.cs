using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
{
    public int AttackCount = 0;
    [Range(1, 9)]
    public int MaxCount;

    public GameObject VFX;
    public BuffItemSO buffItem;
    public override void ExcuteEffect(StatsItemSO parentItem, Unit unit)
    {
        AttackCount++;
        if (AttackCount >= MaxCount)
        {
            Debug.Log("ExcuteEffect Buff Event ");
            if (VFX != null)
                unit.Core.GetCoreComponent<ParticleManager>().StartParticles(VFX, unit.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);
            
            if (buffItem != null)
            {
                Buff buff = new Buff();                
                buff.buffItem = buffItem;
                unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(buffItem.AcquiredSoundEffect);
                unit.gameObject.GetComponent<BuffSystem>().AddBuff(buff);
            }

            AttackCount = 0;
        }
    }
}

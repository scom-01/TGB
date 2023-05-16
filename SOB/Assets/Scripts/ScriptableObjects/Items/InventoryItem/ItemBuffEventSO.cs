using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
{
    private int AttackCount = 0;
    public BuffItemSO buffItem;
    private float startTime = 0;
    public override void ContinouseEffectExcute(StatsItemSO parentItem, Unit unit)
    {
        if (Time.time >= startTime + durationTime) 
        {
            if (buffItem != null)
            {
                Buff buff = new Buff();
                buff.buffItem = buffItem;
                if(unit.Core.GetCoreComponent<SoundEffect>())
                    unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(buffItem.AcquiredSoundEffect);
                if (unit.GetComponent<BuffSystem>())
                    unit.GetComponent<BuffSystem>().AddBuff(buff);
            }
            startTime = Time.time;
        }
    }

    public override void ExecuteEffect(StatsItemSO parentItem, Unit unit)
    {
        AttackCount++;
        if (AttackCount >= MaxCount)
        {
            Debug.Log("ExecuteEffect Buff Event ");
            if (VFX != null)
                unit.Core.GetCoreComponent<ParticleManager>().StartParticles(VFX, unit.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);
            
            if (buffItem != null)
            {
                Buff buff = new Buff();                
                buff.buffItem = buffItem;
                if (unit.Core.GetCoreComponent<SoundEffect>())
                    unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(buffItem.AcquiredSoundEffect);
                if (unit.GetComponent<BuffSystem>())
                    unit.gameObject.GetComponent<BuffSystem>().AddBuff(buff);
            }

            AttackCount = 0;
        }
    }

    public override void ExecuteEffect(StatsItemSO parentItem, Unit unit, Unit enemy)
    {
        AttackCount++;
        if (AttackCount >= MaxCount)
        {
            Debug.Log("ExecuteEffect Buff Event ");
            if (VFX != null)
                unit.Core.GetCoreComponent<ParticleManager>().StartParticles(VFX, enemy.Core.GetCoreComponent<CollisionSenses>().GroundCheck.position);

            if (buffItem != null)
            {
                Buff buff = new Buff();
                buff.buffItem = buffItem;
                if (unit.Core.GetCoreComponent<SoundEffect>())
                    unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(buffItem.AcquiredSoundEffect);
                if (unit.GetComponent<BuffSystem>())
                    unit.gameObject.GetComponent<BuffSystem>().AddBuff(buff);
            }

            AttackCount = 0;
        }
    }

    
}

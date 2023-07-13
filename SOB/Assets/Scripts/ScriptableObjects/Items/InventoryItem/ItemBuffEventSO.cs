using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
{
    public BuffItemSO buffItem;
    public override float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, float startTime)
    {
        if (Time.time >= startTime + itemEffectData.durationTime) 
        {
            if (buffItem != null)
            {
                Buff buff = new Buff();
                var items = buffItem;
                buff.buffItemSO = items;
                //buff.buffItem = items.BuffData;
                //buff.statsData = items.StatsDatas;
                //buff.Health = items.Health;
                //buff.effectData = items.effectData;
                //buff.itemEffects = items.ItemEffects;
                if (unit.Core.GetCoreComponent<SoundEffect>())
                    unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(buffItem.effectData.AcquiredSoundEffect);
                if (unit.GetComponent<BuffSystem>())
                    unit.GetComponent<BuffSystem>().AddBuff(buff);
            }
            startTime = GameManager.Inst.PlayTime;
        }
        return startTime;
    }
}

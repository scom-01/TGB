using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
{
    [Header("Buff Event")]
    public BuffItemSO buffItem;

    private void BuffEvent(Unit unit)
    {
        if (buffItem != null)
        {
            Buff buff = new Buff();
            var items = buffItem;
            buff.buffItemSO = items;
            if (unit.Core.CoreSoundEffect)
                unit.Core.CoreSoundEffect.AudioSpawn(buffItem.effectData.AcquiredSoundEffect);
            if (unit.GetComponent<BuffSystem>() == null)
            {
                return;
            }

            if(unit.GetComponent<BuffSystem>().AddBuff(buff))
            {
                if (itemEffectData.VFX != null)
                    unit.Core.CoreEffectManager.StartEffects(itemEffectData.VFX, unit.Core.CoreCollisionSenses.GroundCheck.position);

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
            BuffEvent(unit);
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
            BuffEvent(unit);
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
            BuffEvent(unit);
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
            BuffEvent(unit);
            startTime = GameManager.Inst.PlayTime;
        }
        return startTime;
    }
}

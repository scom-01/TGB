using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Unity.VisualScripting;

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
                unit.Core.CoreSoundEffect.AudioSpawn(buffItem.InitEffectData.AcquiredSoundEffect);
            if (unit.GetComponent<BuffSystem>() == null)
            {
                return;
            }

            if(unit.GetComponent<BuffSystem>().AddBuff(buff))
            {
                if (itemEffectData.VFX != null)
                    unit.Core.CoreEffectManager.StartEffects(itemEffectData.VFX, unit.Core.CoreCollisionSenses.GroundCenterPos);
            }
        }
    }

    public override int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");

        if (attackCount >= itemEffectData.MaxCount)
        {
            if (enemy != null)
            {
                BuffEvent(enemy);
            }
            else
            {
                BuffEvent(unit);
            }
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
            if(enemy != null)
            {
                BuffEvent(enemy);
            }
            else
            {
                BuffEvent(unit);
            }
            attackCount = 0;
        }
        return attackCount;
    }

    public override float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, Unit enemy, float startTime)
    {
        if (Item_Type != ITEM_TPYE.OnUpdate || Item_Type == ITEM_TPYE.None)
            return startTime;

        if (GameManager.Inst.PlayTime >= startTime + itemEffectData.CooldownTime) 
        {
            if (enemy != null)
            {
                BuffEvent(enemy);
            }
            else
            {
                BuffEvent(unit);
            }
            startTime = GameManager.Inst.PlayTime;
        }
        return startTime;
    }

    public override bool ExecuteOnInit(StatsItemSO parentItem, Unit unit, Unit enemy, bool isInit)
    {
        if (Item_Type != ITEM_TPYE.OnInit || Item_Type == ITEM_TPYE.None)
            return isInit;

        if (isInit)
            return isInit;

        if (enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }
        isInit = false;

        return isInit;
    }

    public override bool ExcuteOnDash(StatsItemSO parentItem, Unit unit, Unit enemy, bool isDash)
    {
        if (Item_Type != ITEM_TPYE.OnDash || Item_Type == ITEM_TPYE.None)
            return isDash;

        if (!isDash)
            return isDash;

        if (enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }

        return isDash;
    }
}

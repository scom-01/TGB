using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemBuffEvent Data")]
public class ItemBuffEventSO : ItemEffectSO
{
    [Header("Buff Event")]
    public BuffItemSO buffItem;

    private void BuffEvent(Unit unit)
    {
        if (buffItem == null)
            return;

        if (Buff.BuffSystemAddBuff(unit, buffItem) == null)
            return;

        SpawnVFX(unit);
        SpawnSFX(unit);
    }

    public override int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");

        if (attackCount < itemEffectData.MaxCount)
            return attackCount;
        
        if (enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }

        attackCount = 0;
        return attackCount;
    }
    public override int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnHit || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");
        if (attackCount < itemEffectData.MaxCount)
            return attackCount;
        
        if(enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }
        attackCount = 0;
        return attackCount;
    }

    public override float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, Unit enemy, float startTime)
    {
        if (Item_Type != ITEM_TPYE.OnUpdate || Item_Type == ITEM_TPYE.None)
            return startTime;

        if (GameManager.Inst.PlayTime < startTime + itemEffectData.CooldownTime) 
            return startTime;

        if (enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }
        startTime = GameManager.Inst.PlayTime;
        return startTime;
    }

    public override bool ExecuteOnInit(StatsItemSO parentItem, Unit unit, Unit enemy, bool isInit)
    {
        if (Item_Type != ITEM_TPYE.OnInit || Item_Type == ITEM_TPYE.None)
            return isInit;

        if (isInit)
            return true;

        if (enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }
        isInit = true;
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

    public override void ExcuteOnMoveMap(StatsItemSO parentItem, Unit unit, Unit enemy)
    {
        if (Item_Type != ITEM_TPYE.OnMoveMap || Item_Type == ITEM_TPYE.None)
            return;

        if (enemy != null)
        {
            BuffEvent(enemy);
        }
        else
        {
            BuffEvent(unit);
        }
    }
}

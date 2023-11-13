using UnityEngine;

[CreateAssetMenu(fileName = "newKnockBackEvent", menuName = "Data/Item Data/ItemKnockBackEventSO Data")]
public class ItemKnockBackEventSO : ItemEffectSO
{
    [Header("KnockBack Event")]
    public Vector2 angle;
    public float strength;
    
    private void KnockBackAction(Unit unit, Unit enemy = null)
    {
        if (enemy == null)
            return;

        if (itemEffectData.VFX != null)
            unit.Core.CoreEffectManager.StartEffectsPos(itemEffectData.VFX, unit.Core.CoreCollisionSenses.GroundCenterPos, itemEffectData.VFX.transform.localScale);

        enemy.Core.CoreKnockBackReceiver.KnockBack(angle, strength, unit.Core.CoreMovement.FancingDirection);
    }

    public override int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");

        if (attackCount >= itemEffectData.MaxCount)
        {
            KnockBackAction(unit, enemy);
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
            KnockBackAction(unit,enemy);
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
            KnockBackAction(unit, enemy);
            startTime = GameManager.Inst.PlayTime;
        }
        return startTime;
    }
}

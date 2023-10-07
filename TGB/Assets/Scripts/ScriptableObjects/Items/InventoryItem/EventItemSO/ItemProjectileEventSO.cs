using TGB.CoreSystem;
using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileAttackEventData", menuName = "Data/Item Data/ItemProjectileAttackEventSO Data")]
public class ItemProjectileEventSO : ItemEffectSO
{
    [Header("Projectile Event")]
    public ProjectileActionData ProjectileActionData;

    private void ProjectileShoot(Unit unit, Unit enemy)
    {
        if (itemEffectData.VFX != null)
            unit.Core.CoreEffectManager.StartEffects(itemEffectData.VFX, unit.Core.CoreCollisionSenses.GroundCenterPos);

        unit.Core.CoreEffectManager.StartProjectileCheck(unit, ProjectileActionData);
    }

    public override int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        attackCount++;
        Debug.Log("ExcuteEffect Attack!");

        if (attackCount >= itemEffectData.MaxCount)
        {
            ProjectileShoot(unit, enemy);
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
            ProjectileShoot(unit, enemy);
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
            ProjectileShoot(unit, enemy);
            startTime = GameManager.Inst.PlayTime;
        }
        return startTime;
    }
}

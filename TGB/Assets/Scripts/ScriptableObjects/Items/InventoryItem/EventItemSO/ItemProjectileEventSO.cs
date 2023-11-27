using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileAttackEventData", menuName = "Data/Item Data/ItemProjectileAttackEventSO Data")]
public class ItemProjectileEventSO : ItemEffectSO
{
    [Header("Projectile Event")]
    public ProjectileActionData ProjectileActionData;

    private void ProjectileShoot(Unit unit, Unit enemy)
    {
        SpawnVFX(unit);
        SpawnSFX(unit);

        unit.Core.CoreEffectManager.StartProjectileCheck(unit, ProjectileActionData);
    }

    public override ItemEffectSet ExcuteEffect(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEffectSet itemEffectSet)
    {
        if (Item_Type != type || Item_Type == ITEM_TPYE.None || itemEffectSet == null)
            return itemEffectSet;

        if (!itemEffectSet.init)
        {
            itemEffectSet.init = true;
        }

        if (GameManager.Inst.PlayTime < itemEffectSet.startTime + itemEffectData.CooldownTime)
        {
            Debug.Log($"itemEffectSet.CoolTime = {GameManager.Inst.PlayTime - itemEffectSet.startTime}");
            return itemEffectSet;
        }

        itemEffectSet.Count++;
        if (itemEffectSet.Count >= itemEffectData.MaxCount)
        {
            ProjectileShoot(unit, enemy);
            itemEffectSet.Count = 0;
            itemEffectSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEffectSet;
    }
}

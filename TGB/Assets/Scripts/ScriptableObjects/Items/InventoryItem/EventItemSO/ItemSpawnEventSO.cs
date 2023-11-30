using UnityEngine;


[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemSpawnEvent Data")]
public class ItemSpawnEventSO : ItemEventSO
{
    [Tooltip("스폰될 오브젝트")]
    [SerializeField] private GameObject Object;
    private bool SpawnObject(Unit unit)
    {
        if (unit == null)
            return false;

        if (Object == null)
            return false;

        SpawnVFX(unit);
        SpawnSFX(unit);

        return true;
    }
    public override ItemEventSet ExcuteEvent(ITEM_TPYE type, StatsItemSO parentItem, Unit unit, Unit enemy, ItemEventSet itemEffectSet)
    {
        if (Item_Type != type || Item_Type == ITEM_TPYE.None || itemEffectSet == null)
            return itemEffectSet;

        if (!itemEffectSet.init)
        {
            itemEffectSet.init = true;
        }

        if (GameManager.Inst.PlayTime < itemEffectSet.startTime + itemEventData.CooldownTime)
        {
            Debug.Log($"itemEffectSet.CoolTime = {GameManager.Inst.PlayTime - itemEffectSet.startTime}");
            return itemEffectSet;
        }

        itemEffectSet.Count++;
        if (itemEffectSet.Count >= itemEventData.MaxCount && itemEventData.Percent >= Random.Range(0f, 100f))
        {
            SpawnObject(unit);
            itemEffectSet.Count = 0;
            itemEffectSet.startTime = GameManager.Inst.PlayTime;
        }

        return itemEffectSet;
    }
}

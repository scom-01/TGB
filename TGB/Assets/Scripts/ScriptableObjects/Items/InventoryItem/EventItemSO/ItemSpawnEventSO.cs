using UnityEngine;


[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemSpawnEvent Data")]
public class ItemSpawnEventSO : ItemEffectSO
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

    public override float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, Unit enemy, float startTime)
    {
        if (Item_Type != ITEM_TPYE.OnUpdate || Item_Type == ITEM_TPYE.None)
            return startTime;

        SpawnObject(unit);

        return startTime;
    }

    public override int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        SpawnObject(unit);

        return attackCount;
    }

    public override int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit Enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnHit || Item_Type == ITEM_TPYE.None)
            return attackCount;

        SpawnObject(unit);

        return attackCount;
    }

    public override bool ExecuteOnInit(StatsItemSO parentItem, Unit unit, Unit enemy, bool isInit)
    {
        if (Item_Type != ITEM_TPYE.OnInit || Item_Type == ITEM_TPYE.None)
            return isInit;

        SpawnObject(unit);

        return isInit;
    }
}

using TGB.Manager;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemEffect Data")]
public abstract class ItemEffectSO : ScriptableObject, IExecuteEffect, IExecuteContinuousEffect
{
    public ITEM_TPYE Item_Type;
    public ItemEffectData itemEffectData;

    /// <summary>
    /// 아이템 획득 시 호출
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit"></param>
    /// <param name="enemy"></param>
    /// <param name="isInit"></param>
    /// <returns></returns>
    public virtual bool ExecuteOnInit(StatsItemSO parentItem, Unit unit, Unit enemy, bool isInit)
    {
        if (Item_Type != ITEM_TPYE.OnInit || Item_Type == ITEM_TPYE.None)
            return isInit;

        return isInit;
    }

    /// <summary>
    /// 액션 시 호출
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit"></param>
    /// <param name="enemy"></param>
    /// <param name="attackCount"></param>
    /// <returns></returns>
    public virtual int ExecuteOnAction(StatsItemSO parentItem, Unit unit, Unit enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnAction || Item_Type == ITEM_TPYE.None)
            return attackCount;

        return attackCount;
    }

    /// <summary>
    /// 공격 온힛 시 호출
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit"></param>
    /// <param name="Enemy"></param>
    /// <param name="attackCount"></param>
    /// <returns></returns>
    public virtual int ExecuteOnHit(StatsItemSO parentItem, Unit unit, Unit Enemy, int attackCount)
    {
        if (Item_Type != ITEM_TPYE.OnHit || Item_Type == ITEM_TPYE.None)
            return attackCount;

        return attackCount;
    }

    /// <summary>
    /// 재사용 대기 시간 마다 호출
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit"></param>
    /// <param name="enemy"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    public virtual float ContinouseEffectExcute(StatsItemSO parentItem, Unit unit, Unit enemy, float startTime)
    {
        if (Item_Type != ITEM_TPYE.OnUpdate || Item_Type == ITEM_TPYE.None)
            return startTime;

        return  startTime;
    }

    /// <summary>
    /// 대쉬 시 호출
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit"></param>
    /// <param name="enemy"></param>
    /// <param name="isDash"></param>
    /// <returns></returns>
    public virtual bool ExcuteOnDash(StatsItemSO parentItem, Unit unit, Unit enemy, bool isDash)
    {
        if (Item_Type != ITEM_TPYE.OnDash || Item_Type == ITEM_TPYE.None)
            return isDash;

        return isDash;
    }

    /// <summary>
    /// Scene 이동 시 호출
    /// </summary>
    /// <param name="parentItem"></param>
    /// <param name="unit"></param>
    /// <param name="enemy"></param>
    public virtual void ExcuteOnMoveMap(StatsItemSO parentItem, Unit unit, Unit enemy)
    {
        if (Item_Type != ITEM_TPYE.OnMoveMap || Item_Type == ITEM_TPYE.None)
            return;
    }
}

[Serializable]
public struct ItemEffectData
{
    /// <summary>
    /// 필요 호출 회수
    /// </summary>
    [Range(1, 99)]
    public int MaxCount;
    /// <summary>
    /// 재사용 대기시간
    /// </summary>
    public float CooldownTime;
    [HideInInspector] public bool isExecute;
    /// <summary>
    /// Effect VFX
    /// </summary>
    public GameObject VFX;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Buff Data")]
public class BuffItemSO : StatsItemSO
{
    public BuffItem_Data BuffData;
}

[Serializable]
public struct BuffItem_Data
{
    [Tooltip("버프 지속시간")]
    public float DurationTime;
    [Tooltip("버프 타입")]
    public EVENT_BUFF_TYPE BuffType;
    [Tooltip("중복여부")]
    public bool isOverlap;
    [Tooltip("중복 카운트")]
    [Range(1,99)] public int BuffCountMax;
    [Tooltip("중복 시 지속시간 초기화")]
    public bool isBuffInit;
}
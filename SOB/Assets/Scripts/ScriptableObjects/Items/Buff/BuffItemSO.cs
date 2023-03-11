using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Buff Data")]
public class BuffItemSO : StatsItemSO
{
    public List<BuffItem_Data> BuffData;
}

[Serializable]
public struct BuffItem_Data
{
    [Tooltip("버프 지속시간")]
    public float DurationTime;
    [Tooltip("버프 타입")]
    public EVENT_BUFF_TYPE BuffType;
}
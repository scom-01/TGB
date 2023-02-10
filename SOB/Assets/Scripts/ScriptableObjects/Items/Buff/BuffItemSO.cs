using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Buff Data")]
public class BuffItemSO : StatsItemSO
{
    [Tooltip("버프 지속시간")]
    public List<float> DurationTime;
    [Tooltip("버프 타입")]
    public List<EVENT_BUFF_TYPE> BuffType;
}

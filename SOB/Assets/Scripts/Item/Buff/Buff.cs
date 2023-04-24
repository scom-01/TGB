using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Buff 
{
    public StatsData statsData;
    public Sprite buffSprite;
    public float durationTime;
    public EVENT_BUFF_TYPE buffType;

    public float startTime;
}
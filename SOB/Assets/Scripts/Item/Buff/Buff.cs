using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Buff 
{
    //public BuffItemSO buffItem;
    public BuffItem_Data buffItem;
    public StatsData statsData;
    public EffectData effectData;
    public ItemData itemData;
    public int CurrBuffCount = 0;
    public List<ItemEffectSO> itemEffects = new List<ItemEffectSO>();
    public float startTime;
}
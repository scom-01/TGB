using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  
public class Buff 
{
    public BuffItemSO buffItemSO;
    //public BuffItem_Data buffItem;
    //public StatsData statsData;
    //[Tooltip("현재 체력 증가값")]
    //public int Health;
    //public EffectData effectData;
    //public ItemData itemData;
    //public List<ItemEffectSO> itemEffects = new List<ItemEffectSO>();

    //Script Object와의 차이점
    public int CurrBuffCount = 0;
    public float startTime;
}
using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Buff 
{
    public BuffItemSO buffItem;
    public int CurrBuffCount = 0;

    public float startTime;
}
using TGB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  
public class Buff 
{
    public BuffItemSO buffItemSO;

    //Script Object와의 차이점
    public int CurrBuffCount = 0;
    public float startTime;
}
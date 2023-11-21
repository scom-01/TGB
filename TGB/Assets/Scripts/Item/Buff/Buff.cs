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

    public static Buff BuffSystemAddBuff(Unit unit, BuffItemSO data)
    {
        if (data == null || unit == null)
            return null;

        Buff buff = new Buff();
        var items = data;
        buff.buffItemSO = items;
        if (unit.Core.CoreSoundEffect && data.InitEffectData.AcquiredSFX.Clip != null)
            unit.Core.CoreSoundEffect.AudioSpawn(data.InitEffectData.AcquiredSFX);

        if (unit.GetComponent<BuffSystem>()?.AddBuff(buff) != null)
        {
            return buff;
        }

        return null;
    }
}
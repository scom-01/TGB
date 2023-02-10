using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff 
{
    public BuffItemSO BuffItemData;

    private Unit unit;

    

    //public void PlayBuff()
    //{
    //    if (ItemData.BuffType.Length > 0)
    //    {
    //        for (int i = 0; i < ItemData.BuffType.Length; i++)
    //        {
    //            if (ItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_Buff.ToString())
    //            {
    //                StartCoroutine(
    //                    E_Increase
    //                        (
    //                        ItemData.BuffName[i].ToString(),
    //                        ItemData.BuffValue[i],
    //                        ItemData.BuffDurationTime[i]
    //                        )
    //                    );
    //            }
    //            else if (ItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_DeBuff.ToString())
    //            {
    //                StartCoroutine(
    //                    E_Decrease
    //                        (
    //                        ItemData.BuffName[i].ToString(),
    //                        ItemData.BuffValue[i],
    //                        ItemData.BuffDurationTime[i]
    //                        )
    //                    );
    //            }
    //        }
    //    }
    //}
    //#region IEnumerator
    //IEnumerator E_Increase(string statName, float value, float duration)
    //{
    //    if (unit != null)
    //    {
    //        unit.Core.GetCoreComponent<UnitStats>().Increase(statName, value);
    //        yield return new WaitForSeconds(duration);
    //        unit.Core.GetCoreComponent<UnitStats>().Decrease(statName, value);
    //        yield break;
    //    }

    //    yield break;
    //}

    //IEnumerator E_Decrease(string statName, float value, float duration)
    //{
    //    if (unit != null)
    //    {
    //        unit.Core.GetCoreComponent<UnitStats>().Decrease(statName, value);
    //        yield return new WaitForSeconds(duration);
    //        unit.Core.GetCoreComponent<UnitStats>().Increase(statName, value);
    //        yield break;
    //    }

    //    yield break;
    //}
    //#endregion

}
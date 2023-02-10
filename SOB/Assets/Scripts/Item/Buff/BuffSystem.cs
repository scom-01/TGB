using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;


public class BuffSystem : MonoBehaviour
{
    public List<Buff> buffs =new List<Buff>();
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {

    }

    public void AddBuff(Buff buff)
    {
        //if (buffs.Contains(buff))
        //{
        //    Debug.Log($"Contians {buff.name}, fail add");
        //}
        //else
        {
            buffs.Add(buff);
            PlayBuff(buff);
            //SetStat(buff.ItemData.ItemData.commomData, true);
            //StartCoroutine(ActiveBuff(buff.ItemData.BuffDurationTime));
            //Debug.Log($"Change UnitStats {unit.Core.GetCoreComponent<UnitStats>().CommonData}");
        }
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
        {
            ClearBuff(buff);
            buffs.Remove(buff);
        }
        else
        {
            Debug.Log($"Not Contians {buff}, fail remove");
        }
    }

    public void PlayBuff(Buff buff)
    {
        if (buff.BuffItemData.CommonDatas.Count > 0)
        {
            for (int i = 0; i < buff.BuffItemData.CommonDatas.Count; i++)
            {
                StartCoroutine(ChangeStats(buff, buff.BuffItemData.CommonDatas[i], buff.BuffItemData.DurationTime[i]));
                //if (buff.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_Buff.ToString())
                //{
                //    StartCoroutine(
                //        E_Increase
                //            (
                //            buff,
                //            buff.BuffItemData.BuffName[i].ToString(),
                //            buff.BuffItemData.BuffValue[i],
                //            buff.BuffItemData.BuffDurationTime[i]
                //            )
                //        );
                //}
                //else if (buff.BuffItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_DeBuff.ToString())
                //{
                //    StartCoroutine(
                //        E_Decrease
                //            (
                //            buff,
                //            buff.BuffItemData.BuffName[i].ToString(),
                //            buff.BuffItemData.BuffValue[i],
                //            buff.BuffItemData.BuffDurationTime[i]
                //            )
                //        );
                //}
            }
        }
    }

    public void ClearBuff(Buff buff)
    {
        if (buff.BuffItemData.CommonDatas.Count > 0)
        {
            for (int i = 0; i < buff.BuffItemData.CommonDatas.Count; i++)
            {
                StopCoroutine(ChangeStats(buff, buff.BuffItemData.CommonDatas[i], buff.BuffItemData.DurationTime[i]));
                //if (buff.BuffItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_Buff.ToString())
                //{   
                //    StopCoroutine(
                //        E_Increase
                //            (
                //            buff,
                //            buff.BuffItemData.BuffName[i].ToString(),
                //            buff.BuffItemData.BuffValue[i],
                //            buff.BuffItemData.BuffDurationTime[i]
                //            )
                //        );
                //}
                //else if (buff.BuffItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_DeBuff.ToString())
                //{
                //    StopCoroutine(
                //        E_Decrease
                //            (
                //            buff,
                //            buff.BuffItemData.BuffName[i].ToString(),
                //            buff.BuffItemData.BuffValue[i],
                //            buff.BuffItemData.BuffDurationTime[i]
                //            )
                //        );
                //}
            }
        }

    }
    #region IEnumerator

    IEnumerator ChangeStats(Buff buff, CommonData commonData, float duration)
    {
        if(unit != null)
        {
            unit.Core.GetCoreComponent<UnitStats>().CommonData += commonData;
            yield return new WaitForSeconds(duration);
            unit.Core.GetCoreComponent<UnitStats>().CommonData += commonData * -1f;
            RemoveBuff(buff);
            yield break;
        }
        Debug.LogWarning($"{buff} is not have unit");
        yield break;
    }
    IEnumerator E_Increase(Buff buff, string statName, float value, float duration)
    {
        if (unit != null)
        {
            unit.Core.GetCoreComponent<UnitStats>().Increase(statName, value);
            yield return new WaitForSeconds(duration);
            unit.Core.GetCoreComponent<UnitStats>().Decrease(statName, value);
            RemoveBuff(buff);
            yield break;
        }

        yield break;
    }

    IEnumerator E_Decrease(Buff buff, string statName, float value, float duration)
    {
        if (unit != null)
        {
            unit.Core.GetCoreComponent<UnitStats>().Decrease(statName, value);
            yield return new WaitForSeconds(duration);
            unit.Core.GetCoreComponent<UnitStats>().Increase(statName, value);
            RemoveBuff(buff);
            yield break;
        }

        yield break;
    }
    #endregion
}

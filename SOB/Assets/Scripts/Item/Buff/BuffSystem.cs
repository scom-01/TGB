using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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
        if (buff.BuffItemData.StatsDatas.Count > 0)
        {
            for (int i = 0; i < buff.BuffItemData.StatsDatas.Count; i++)
            {
                StartCoroutine(ChangeStats(buff, buff.BuffItemData.StatsDatas[i], buff.BuffItemData.DurationTime[i]));
            }
        }
    }

    public void ClearBuff(Buff buff)
    {
        if (buff.BuffItemData.StatsDatas.Count > 0)
        {
            for (int i = 0; i < buff.BuffItemData.StatsDatas.Count; i++)
            {
                StopCoroutine(ChangeStats(buff, buff.BuffItemData.StatsDatas[i], buff.BuffItemData.DurationTime[i]));
            }
        }

    }
    #region IEnumerator

    IEnumerator ChangeStats(Buff buff, StatsData statsData, float duration)
    {
        if(unit != null)
        {
            unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData;
            if (duration == 999.0f)
            {
                Debug.LogWarning("duration is 999");
                yield break;
            }
            yield return new WaitForSeconds(duration);
            unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData * -1f;
            RemoveBuff(buff);
            yield break;
        }
        Debug.LogWarning($"{buff} is not have unit");
        yield break;
    }    
    #endregion
}

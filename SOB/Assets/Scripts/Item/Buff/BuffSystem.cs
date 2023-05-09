using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffSystem : MonoBehaviour
{
    public List<Buff> buffs = new List<Buff>();
    public List<BuffItemSO> buffItems = new List<BuffItemSO>();
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
    private void Update()
    {
        if (buffs.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < buffs.Count;)
        {
            
            if (Time.time >= buffs[i].startTime + buffs[i].buffItem.BuffData.DurationTime)
            {
                unit.Core.GetCoreComponent<UnitStats>().StatsData += buffs[i].buffItem.StatsDatas * -1f * buffs[i].CurrBuffCount;
                if (unit.Core.GetCoreComponent<UnitStats>().CurrentHealth > buffs[i].buffItem.StatsDatas.MaxHealth)
                {
                    unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += buffs[i].buffItem.StatsDatas.MaxHealth * -1f * buffs[i].CurrBuffCount;
                }
                buffs.RemoveAt(i);
                if (buffs.Count <= 0)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {

            }
            i++;
        }
    }

    public void AddBuff(Buff buff)
    {
        buff.startTime = Time.time;   
        
        if(buffItems.Contains(buff.buffItem))
        {
            for (int i = 0; i < buffItems.Count; i++)
            {
                if (buffItems[i] == buff.buffItem)
                {
                    //지속효과 초기화
                    if (buffItems[i].BuffData.isBuffInit)
                    {
                        buffs[i].startTime = Time.time;
                    }

                    //중복 X
                    if (!buffItems[i].BuffData.isOverlap)
                    {
                        return;
                    }

                    //중복 최대치 
                    if (buffs[i].CurrBuffCount >= buffItems[i].BuffData.BuffCountMax)
                    {
                        return;
                    }

                    buffs[i].CurrBuffCount++;
                }
            }
        }
        else
        {
            buffs?.Add(buff);
            buff.CurrBuffCount++;
            buffItems?.Add(buff.buffItem);
            GameManager.Inst?.MainUI?.MainPanel?.BuffPanelSystem.BuffPanelAdd(buff);
        }

        unit.Core.GetCoreComponent<UnitStats>().StatsData += buff.buffItem.StatsDatas;
        if (buff.buffItem.StatsDatas.MaxHealth != 0.0f)
        {
            unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += buff.buffItem.StatsDatas.MaxHealth;
        }
        //PlayBuff(buff);
    }


    //무쓸모 예정
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

    //무쓸모 예정
    public void PlayBuff(Buff buff)
    {
        StartCoroutine(ChangeStats(buff, buff.buffItem.StatsDatas, buff.buffItem.BuffData.DurationTime));
    }
    //무쓸모 예정
    public void ClearBuff(Buff buff)
    {
        StopCoroutine(ChangeStats(buff, buff.buffItem.StatsDatas, buff.buffItem.BuffData.DurationTime));
    }

    //무쓸모 예정
    #region IEnumerator

    IEnumerator ChangeStats(Buff buff, StatsData statsData, float duration)
    {
        if (unit != null)
        {
            unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData;
            if (statsData.MaxHealth != 0.0f)
            {
                unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += statsData.MaxHealth;
            }
            if (duration == 999.0f)
            {
                Debug.LogWarning("duration is 999");
                yield break;
            }
            yield return new WaitForSeconds(duration);
            unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData * -1f;
            if (statsData.MaxHealth != 0.0f)
            {
                unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += statsData.MaxHealth * -1f;
            }
            RemoveBuff(buff);
            yield break;
        }
        Debug.LogWarning($"{buff} is not have unit");
        yield break;
    }
    #endregion
}

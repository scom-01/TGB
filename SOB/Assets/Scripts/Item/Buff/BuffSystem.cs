using Cinemachine.Editor;
using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffSystem : MonoBehaviour
{
    public List<Buff> buffs = new List<Buff>();
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if(buffs == null)
        {
            buffs = new List<Buff>();
            return;
        }
        if (buffs.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < buffs.Count;)
        {            
            if (GameManager.Inst?.PlayTime >= buffs[i].startTime + buffs[i].buffItemSO.BuffData.DurationTime)
            {
                Debug.Log($"Time = {Time.time}");
                Debug.Log($"GlobalTime ={GameManager.Inst?.PlayTime}");
                unit.Core.GetCoreComponent<UnitStats>().StatsData += buffs[i].buffItemSO.StatsDatas* -1f * buffs[i].CurrBuffCount;

                ////현재 체력이 버프의 체력증가 값보다 클 때 증가시켜줬던 체력을 빼앗는 코드
                //if (unit.Core.GetCoreComponent<UnitStats>().CurrentHealth > buffs[i].Health)
                //{
                //    unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += buffs[i].Health * -1f * buffs[i].CurrBuffCount;
                //}
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
        buff.startTime = GameManager.Inst.PlayTime;   
        
        for(int i = 0; i < buffs.Count;i++)
        {
            if (buffs[i].buffItemSO == buff.buffItemSO)
            {
                //버프 초기화
                if (buffs[i].buffItemSO.BuffData.isBuffInit)
                {
                    buffs[i].startTime = GameManager.Inst.PlayTime;
                }

                //중복 X
                if (!buffs[i].buffItemSO.BuffData.isOverlap)
                {
                    return;
                }

                //중복 최대치 
                if (buffs[i].CurrBuffCount >= buffs[i].buffItemSO.BuffData.BuffCountMax)
                {
                    return;
                }

                buffs[i].CurrBuffCount++;
                return;
            }
        }
        
        buffs?.Add(buff);
        buff.CurrBuffCount++;
        if (unit.GetType() == typeof(Player))
        {
            GameManager.Inst?.MainUI?.MainPanel?.BuffPanelSystem.BuffPanelAdd(buff);
        }
        unit.Core.GetCoreComponent<UnitStats>().StatsData += buff.buffItemSO.StatsDatas;
        if (buff.buffItemSO.Health != 0.0f)
        {
            unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += buff.buffItemSO.Health;
        }
        //PlayBuff(buff);
    }

    public void SetBuff()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (unit.GetType() == typeof(Player))
            {
                GameManager.Inst?.MainUI?.MainPanel?.BuffPanelSystem.BuffPanelAdd(buffs[i]);
            }
        }      
    }

    ////무쓸모 예정
    //public void RemoveBuff(Buff buff)
    //{
    //    if (buffs.Contains(buff))
    //    {
    //        ClearBuff(buff);
    //        buffs.Remove(buff);
    //    }
    //    else
    //    {
    //        Debug.Log($"Not Contians {buff}, fail remove");
    //    }
    //}

    ////무쓸모 예정
    //public void PlayBuff(Buff buff)
    //{
    //    StartCoroutine(ChangeStats(buff, buff.statsData, buff.buffItem.DurationTime));
    //}
    ////무쓸모 예정
    //public void ClearBuff(Buff buff)
    //{
    //    StopCoroutine(ChangeStats(buff, buff.statsData, buff.buffItem.DurationTime));
    //}

    ////무쓸모 예정
    //#region IEnumerator

    //IEnumerator ChangeStats(Buff buff, StatsData statsData, float duration)
    //{
    //    if (unit != null)
    //    {
    //        unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData;
    //        if (statsData.MaxHealth != 0.0f)
    //        {
    //            unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += statsData.MaxHealth;
    //        }
    //        if (duration == 999.0f)
    //        {
    //            Debug.LogWarning("duration is 999");
    //            yield break;
    //        }
    //        yield return new WaitForSeconds(duration);
    //        unit.Core.GetCoreComponent<UnitStats>().StatsData += statsData * -1f;
    //        if (statsData.MaxHealth != 0.0f)
    //        {
    //            unit.Core.GetCoreComponent<UnitStats>().CurrentHealth += statsData.MaxHealth * -1f;
    //        }
    //        RemoveBuff(buff);
    //        yield break;
    //    }
    //    Debug.LogWarning($"{buff} is not have unit");
    //    yield break;
    //}
    //#endregion
}

using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BuffSystem : MonoBehaviour
{
    public List<Buff> buffs = new List<Buff>();
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
        buff.startTime = Time.time;
        buffs.Add(buff);
        GameManager.Inst.MainUI.BuffPanelSystem.BuffPanelAdd(buff);
        PlayBuff(buff);
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
        StartCoroutine(ChangeStats(buff, buff.statsData, buff.durationTime));
    }

    public void ClearBuff(Buff buff)
    {
        StopCoroutine(ChangeStats(buff, buff.statsData, buff.durationTime));
    }
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

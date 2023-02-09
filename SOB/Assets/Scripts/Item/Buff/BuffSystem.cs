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
            buffs.Remove(buff);
        }
        else
        {
            Debug.Log($"Not Contians {buff}, fail remove");
        }
    }

    public void PlayBuff(Buff buff)
    {
        if (buff.ItemData.BuffType.Length > 0)
        {
            for (int i = 0; i < buff.ItemData.BuffType.Length; i++)
            {
                if (buff.ItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_Buff.ToString())
                {
                    StartCoroutine(
                        E_Increase
                            (
                            buff,
                            buff.ItemData.BuffName[i].ToString(),
                            buff.ItemData.BuffValue[i],
                            buff.ItemData.BuffDurationTime[i]
                            )
                        );
                }
                else if (buff.ItemData.BuffType[i].ToString() == EVENT_BUFF_TYPE.E_DeBuff.ToString())
                {
                    StartCoroutine(
                        E_Decrease
                            (
                            buff,
                            buff.ItemData.BuffName[i].ToString(),
                            buff.ItemData.BuffValue[i],
                            buff.ItemData.BuffDurationTime[i]
                            )
                        );
                }
            }
        }
    }

    #region IEnumerator
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

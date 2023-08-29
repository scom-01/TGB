using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UpgradeBlessing : MonoBehaviour
{
    public void Upgrade(Blessing_Stats_TYPE type)
    {        
        switch (type)
        {
            case Blessing_Stats_TYPE.Bless_Agg:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[0]++;
                break;
            case Blessing_Stats_TYPE.Bless_Def:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[1]++;
                break;
            case Blessing_Stats_TYPE.Bless_Speed:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[2]++;
                break;
            case Blessing_Stats_TYPE.Bless_Critical:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[3]++;
                break;
            case Blessing_Stats_TYPE.Bless_Elemental:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[4]++;
                break;
        }
    }
}

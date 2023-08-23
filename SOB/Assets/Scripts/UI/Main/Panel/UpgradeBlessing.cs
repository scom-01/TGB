using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBlessing : MonoBehaviour
{
    public void Upgrade(Blessing_Stats_TYPE type)
    {
        switch (type)
        {
            case Blessing_Stats_TYPE.Bless_Agg:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Agg_Lv++;
                break;
            case Blessing_Stats_TYPE.Bless_Def:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Def_Lv++;
                break;
            case Blessing_Stats_TYPE.Bless_Speed:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Speed_Lv++;
                break;
            case Blessing_Stats_TYPE.Bless_Critical:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Critical_Lv++;
                break;
            case Blessing_Stats_TYPE.Bless_Elemental:
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Elemental_Lv++;
                break;
        }
    }
}

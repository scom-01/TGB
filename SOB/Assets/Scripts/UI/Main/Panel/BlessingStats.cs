using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BlessingStats : Stats
{
    public override float variable
    {
        get
        {
            return TypeStats_Lv;
        }
    }
    public override string TypeStr
    {
        get
        {
            return Type.ToString();
        }
    }
    [SerializeField] private Blessing_Stats_TYPE Type;
    [SerializeField] private TextMeshProUGUI StatsPowerTxt;

    private int TypeStats_Lv
    {
        get
        {
            int stats = 0;
            if (DataManager.Inst == null)
                return 0;
            switch (Type)
            {
                case Blessing_Stats_TYPE.Bless_Agg:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Agg_Lv;
                    break;                    
                case Blessing_Stats_TYPE.Bless_Def:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Def_Lv;
                    break;
                case Blessing_Stats_TYPE.Bless_Speed:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Speed_Lv;
                    break;
                case Blessing_Stats_TYPE.Bless_Critical:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Critical_Lv;
                    break;
                case Blessing_Stats_TYPE.Bless_Elemental:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Blessing_Elemental_Lv;
                    break;
            }
            return stats;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}

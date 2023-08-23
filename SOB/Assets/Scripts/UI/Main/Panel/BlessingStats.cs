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
    public override Sprite TypeSprite
    {
        get
        {
            return m_TypeSprite;
        }
    }

    [SerializeField] private Blessing_Stats_TYPE Type;
    [SerializeField] private Image StatsImg;

    private SpriteAtlas SpriteAtlas
    {
        get
        {
            if (m_SpriteAtlas == null)
            {
                m_SpriteAtlas = Resources.Load<SpriteAtlas>(GlobalValue.Sprites_UI_Path + "/Stats_Atlas");
            }
            return m_SpriteAtlas;
        }
    }
    private SpriteAtlas m_SpriteAtlas;
    [SerializeField] private Sprite m_TypeSprite
    {
        get
        {
            if (SpriteAtlas.spriteCount > 0)
            {
                return SpriteAtlas.GetSprite(Type.ToString());
            }

            return Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + Type.ToString());
        }
    }

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

using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
        set
        {
            TypeStats_Lv = (int)value;
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
    [SerializeField]
    private Sprite m_TypeSprite
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
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[0];
                    break;
                case Blessing_Stats_TYPE.Bless_Def:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[1];
                    break;
                case Blessing_Stats_TYPE.Bless_Speed:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[2];
                    break;
                case Blessing_Stats_TYPE.Bless_Critical:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[3];
                    break;
                case Blessing_Stats_TYPE.Bless_Elemental:
                    stats = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[4];
                    break;
            }
            return stats;
        }
        set
        {
            switch (Type)
            {
                case Blessing_Stats_TYPE.Bless_Agg:
                    DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[0] = value;
                    break;
                case Blessing_Stats_TYPE.Bless_Def:
                    DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[1] = value;
                    break;
                case Blessing_Stats_TYPE.Bless_Speed:
                    DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[2] = value;
                    break;
                case Blessing_Stats_TYPE.Bless_Critical:
                    DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[3] = value;
                    break;
                case Blessing_Stats_TYPE.Bless_Elemental:
                    DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.Bless[4] = value;
                    break;
            }
        }
    }
    private Blessing_Stats_TYPE Old_Type;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        if (LocalStringEvent != null)
        {
            LocalStringEvent.StringReference.SetReference("Stats_Table", Type.ToString());
        }

        if (StatsImg != null && m_TypeSprite != null && StatsImg.sprite != m_TypeSprite)
        {
            StatsImg.sprite = m_TypeSprite;
        }
    }
    private void Update()
    {
        if (Old_Type == Type)
            return;

        if (LocalStringEvent != null)
        {
            LocalStringEvent.StringReference.SetReference("Stats_Table", Type.ToString());
        }

        if (StatsImg != null && m_TypeSprite != null && StatsImg.sprite != m_TypeSprite)
        {
            StatsImg.sprite = m_TypeSprite;
        }
        Old_Type = Type;
    }
}

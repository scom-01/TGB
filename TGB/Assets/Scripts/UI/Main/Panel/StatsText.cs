using TGB.CoreSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StatsText : Stats
{
    private Unit Unit
    {
        get
        {
            if (unit == null)
            {
                unit = GameManager.Inst?.StageManager?.player;

                if (unit != null)
                {
                    Stats.OnChangeHealth -= UpdateStats;
                    Stats.OnChangeHealth += UpdateStats;                    
                    Stats.OnChangeStats -= UpdateStats;
                    Stats.OnChangeStats += UpdateStats;
                    UpdateStats();
                }
            }
            return unit;
        }
    }
    private Unit unit;
    private UnitStats Stats
    {
        get
        {
            if (stats == null)
            {
                if (Unit == null)
                {
                    return null;
                }
                stats = Unit.Core.CoreUnitStats;
            }
            return stats;
        }
    }
    private UnitStats stats;
    public override float variable
    {
        get
        {
            return m_TypeStats;
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
    [SerializeField] private Stats_TYPE Type;
    [SerializeField] private Image Img;    
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

    private Sprite m_TypeSprite
    {
        get
        {
            if(SpriteAtlas.spriteCount > 0)
            {
                return SpriteAtlas.GetSprite(Type.ToString());
            }

            return Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + Type.ToString());
        }
    }

    [SerializeField] private TextMeshProUGUI StatsPowerTxt;
    [SerializeField] private bool Showcolon;
    [SerializeField] private bool isLeftcolon;
    [SerializeField] private bool HidePercent;

    private float m_TypeStats
    {
        get
        {
            float stats = 0f;
            if (GameManager.Inst.StageManager == null)
                return -1f;
            switch(Type)
            {
                case Stats_TYPE.PhysicsAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.PhysicsAggressivePer;
                    break;
                case Stats_TYPE.MagicAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.MagicAggressivePer;
                    break;
                case Stats_TYPE.PhysicsDef:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.PhysicsDefensivePer;
                    break;
                case Stats_TYPE.MagicDef:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.MagicDefensivePer;
                    break;
                case Stats_TYPE.ElementalAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.ElementalAggressivePer;
                    break;
                case Stats_TYPE.ElementalDef:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.ElementalDefensivePer;
                    break;
                case Stats_TYPE.AttackSpeed:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.AttackSpeedPer;
                    break;
                case Stats_TYPE.CriticalPer:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.CriticalPer;
                    break;
                case Stats_TYPE.AdditionalCritical:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.AdditionalCriticalPer;
                    break;
                case Stats_TYPE.MaxHealth:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.MaxHealth;
                    break;
                case Stats_TYPE.MoveSpeed:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.MovementVEL_Per;
                    break;
                case Stats_TYPE.JumpVelocity:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.CalculStatsData.JumpVEL_Per;
                    break;
            }
            return stats;
        }
    }

    private float OldStats = -1f;

    private Canvas Canvas
    {
        get
        {
            if(m_canvas == null)
            {
                m_canvas = this.GetComponentInParent<Canvas>();
            }
            return m_canvas;
        }
    }
    private Canvas m_canvas;

    private void OnEnable()
    {
        if (GameManager.Inst?.StageManager?.player != null)
        {
            GameManager.Inst.StageManager.player.Core.CoreUnitStats.OnChangeHealth -= UpdateStats;
            GameManager.Inst.StageManager.player.Core.CoreUnitStats.OnChangeHealth += UpdateStats;
        }
    }
    private void OnDisable()
    {
        if (GameManager.Inst?.StageManager?.player != null)
        {
            GameManager.Inst.StageManager.player.Core.CoreUnitStats.OnChangeHealth += UpdateStats;
        }
    }

    private void Update()
    {
        if (Unit == null)
            return;
    }

    // Update is called once per frame
    private void UpdateStats()
    {
        if (LocalStringEvent != null)
        {
            LocalStringEvent.StringReference.SetReference("Stats_Table", Type.ToString());
        }

        if (StatsPowerTxt != null && OldStats != m_TypeStats)
        {
            StatsPowerTxt.text = isLeftcolon ? ((Showcolon ? " : " : "") + m_TypeStats.ToString("F0") + (HidePercent ? "" : " %")) : m_TypeStats.ToString("F0") + (HidePercent ? "" : " %") + (Showcolon ? " : " : "");
            OldStats = m_TypeStats;
        }

        if (Img != null && m_TypeSprite != null && Img.sprite != m_TypeSprite)
        {
            Img.sprite = m_TypeSprite;
        }
    }
    [ContextMenu("Set Img")]
    private void SetImg()
    {
        if (this.GetComponent<Image>() != null)
        {
            Img = this.GetComponent<Image>();
        }

        if(Img == null)
        {
            Img = this.GetComponentInChildren<Image>();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.U2D;
using UnityEngine.UI;

public class StatsText : Stats
{
    public override float variable
    {
        get
        {
            return TypeStats;
        }
    }
    public override string TypeStr
    {
        get
        {
            return Type.ToString();
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

    private Sprite TypeSprite
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

    private float TypeStats
    {
        get
        {
            float stats = 0f;
            if (GameManager.Inst.StageManager == null)
                return -1f;
            switch(Type)
            {
                case Stats_TYPE.PhysicsAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.PhysicsAggressivePer;
                    break;
                case Stats_TYPE.MagicAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.MagicAggressivePer;
                    break;
                case Stats_TYPE.PhysicsDef:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.PhysicsDefensivePer;
                    break;
                case Stats_TYPE.MagicDef:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.MagicDefensivePer;
                    break;
                case Stats_TYPE.ElementalAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.ElementalAggressivePer;
                    break;
                case Stats_TYPE.ElementalDef:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.ElementalDefensivePer;
                    break;
                case Stats_TYPE.AttackSpeed:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.AttackSpeedPer;
                    break;
                case Stats_TYPE.CriticalPer:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.CriticalPer;
                    break;
                case Stats_TYPE.AdditionalCritical:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.AdditionalCriticalPer;
                    break;
                case Stats_TYPE.MaxHealth:
                    stats = GameManager.Inst.StageManager.player.Core.CoreUnitStats.MaxHealth;
                    break;
                case Stats_TYPE.MoveSpeed:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.MoveSpeed;
                    break;
                case Stats_TYPE.JumpVelocity:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.CoreUnitStats.JumpVelocity;
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
    // Update is called once per frame
    void Update()
    {
        if(!Canvas.enabled)
        {
            return;
        }

        if (LocalStringEvent != null)
        {
            LocalStringEvent.StringReference.SetReference("Stats_Table", Type.ToString());
        }

        if (StatsPowerTxt != null)
        {
            if (OldStats != TypeStats)
            {                
                StatsPowerTxt.text = TypeStats.ToString() + " %";
                OldStats = TypeStats;
            }
        }

        if (Img != null && TypeSprite != null && Img.sprite != TypeSprite)
        {
            Img.sprite = TypeSprite;
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

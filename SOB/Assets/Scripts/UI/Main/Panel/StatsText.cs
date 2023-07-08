using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class StatsText : MonoBehaviour
{
    [SerializeField] private Stats_TYPE Type;
    [SerializeField] private Image Img;
    private Sprite TypeSprite
    {
        get
        {
            return Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + Type.ToString());
        }
    }

    [SerializeField] private TextMeshProUGUI StatsPowerTxt;
    [SerializeField] private LocalizeStringEvent StatsTxtLocalizeStringEvent;

    private float TypeStats
    {
        get
        {
            float stats = 0f;
            if (GameManager.Inst?.StageManager == null)
                return -1f;
            switch(Type)
            {
                case Stats_TYPE.PhysicsAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().PhysicsAggressivePer;
                    break;
                case Stats_TYPE.MagicAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().MagicAggressivePer;
                    break;
                case Stats_TYPE.PhysicsDef:
                    stats = GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().PhysicsDefensivePer;
                    break;
                case Stats_TYPE.MagicDef:
                    stats = GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().MagicDefensivePer;
                    break;
                case Stats_TYPE.ElementalAgg:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().ElementalAggressivePer;
                    break;
                case Stats_TYPE.ElementalDef:
                    stats = GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().ElementalDefensivePer;
                    break;
                case Stats_TYPE.AttackSpeed:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().AttackSpeedPer;
                    break;
                case Stats_TYPE.MaxHealth:
                    stats = GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().MaxHealth;
                    break;
                case Stats_TYPE.MoveSpeed:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().MoveSpeed;
                    break;
                case Stats_TYPE.JumpVelocity:
                    stats = 100f + GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().JumpVelocity;
                    break;
            }
            return stats;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (StatsTxtLocalizeStringEvent != null)
        {
            StatsTxtLocalizeStringEvent.StringReference.SetReference("Stats_Table", Type.ToString());
        }

        if (StatsPowerTxt != null)
        {            
            StatsPowerTxt.text = TypeStats.ToString() + " %";
        }

        if (Img != null && TypeSprite != null)
        {
            Img.sprite = TypeSprite;
        }
    }
}

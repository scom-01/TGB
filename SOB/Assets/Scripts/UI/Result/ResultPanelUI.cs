using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelUI : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI PhysicsAgg_Txt;
    [SerializeField] private TextMeshProUGUI MagicAgg_Txt;
    [SerializeField] private TextMeshProUGUI PhysicsDef_Txt;
    [SerializeField] private TextMeshProUGUI MagicDef_Txt;
    [SerializeField] private TextMeshProUGUI ElementalAgg_Txt;
    [SerializeField] private TextMeshProUGUI ElementalDef_Txt;
    [SerializeField] private TextMeshProUGUI AttackSpeed_Txt;
    [SerializeField] private TextMeshProUGUI MaxHealth_Txt;

    [Header("Goods")]
    [SerializeField] private TextMeshProUGUI Goods_Gold_Txt;
    [SerializeField] private TextMeshProUGUI Goods_ES_Txt;

    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI Enemy_Small_Txt;
    [SerializeField] private TextMeshProUGUI Enemy_Normal_Txt;
    [SerializeField] private TextMeshProUGUI Enemy_Boss_Txt;

    [Header("Item")]
    [SerializeField] private List<Image> ItemList;

    public void UpdateResultPanel(StatsData stats)
    {
        //if (PhysicsAgg_Txt != null)
        //    PhysicsAgg_Txt.text = string.Format($"{100f + stats.PhysicsAggressivePer} %");
        //if (MagicAgg_Txt != null)
        //    MagicAgg_Txt.text = string.Format($"{100f + stats.MagicAggressivePer} %");
        //if (PhysicsDef_Txt != null)
        //    PhysicsDef_Txt.text = string.Format($"{stats.PhysicsDefensivePer} %");
        //if (MagicDef_Txt != null)
        //    MagicDef_Txt.text = string.Format($"{stats.MagicDefensivePer} %");
        //if (ElementalAgg_Txt != null)
        //    ElementalAgg_Txt.text = string.Format($"{100f + stats.ElementalAggressivePer} %");
        //if (ElementalDef_Txt != null)
        //    ElementalDef_Txt.text = string.Format($"{stats.ElementalDefensivePer} %");
        //if (AttackSpeed_Txt != null)
        //    AttackSpeed_Txt.text = string.Format($"{100f + stats.AttackSpeedPer} %");
        //if (MaxHealth_Txt != null)
        //    MaxHealth_Txt.text = string.Format($"{stats.MaxHealth}");

        //if (DataManager.Inst != null)
        //{
        //    if (Goods_Gold_Txt != null)
        //        Goods_Gold_Txt.text = string.Format($"{DataManager.Inst.GoldCount}");
        //    if (Goods_ES_Txt != null)
        //        Goods_ES_Txt.text = string.Format($"{DataManager.Inst.ElementalsculptureCount}");
        //}
    }
}


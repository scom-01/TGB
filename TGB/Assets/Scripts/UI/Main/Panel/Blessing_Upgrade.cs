using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class Blessing_Upgrade : MonoBehaviour, IUI_Select
{
    private Stats Stats;
    [SerializeField] private Image Icon;
    [SerializeField] private LocalizeStringEvent LocalizeStringEvent_Descript;
    [SerializeField] private LocalizeStatsDataUpdater LocalizeStatsDataUpdater;

    [SerializeField] private TMP_Text TMP_BlessingCost;
    [SerializeField] private LocalizeStringEvent LocalizeStringEvent_BlessingCost;
    [SerializeField] private int Max_level;
    [SerializeField] private Button Blessing_Btn;
    private int piece;
    //OnClick
    public void Set(Stats _stats)
    {
        Stats = _stats;

        if (Stats.TypeSprite != null)
        {
            Icon.sprite = Stats.TypeSprite;
        }
        LocalizeStringEvent_Descript.StringReference.SetReference("Stats_Table", Stats.TypeStr + "_Descript");
        LocalizeStatsDataUpdater.SetStats(_stats);
        LocalizeStatsDataUpdater.UpdateLocalizedText();
        Set_Blessing();
    }

    private void Set_Blessing()
    {
        piece = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.hammer_piece;

        // 텍스트 업데이트
        string localizedText = LocalizeStringEvent_BlessingCost.StringReference.GetLocalizedString();
        localizedText += string.Format($" : <color={(CheckUpgrade(piece) ? "green" : "red")}>{piece.ToString("N0")}</color> : {(GlobalValue.Bless_Inflation * (Stats.variable + 1)).ToString("N0")}");

        if (TMP_BlessingCost != null)
            TMP_BlessingCost.text = localizedText;
    }
    public void Upgrade()
    {
        if (!CheckUpgrade(piece))
        {
            return;
        }
        DataManager.Inst.CalculateGoods(GOODS_TPYE.HammerShards, -GlobalValue.Bless_Inflation * (int)(Stats.variable + 1));
        Stats.variable++;
        Debug.Log($"{Stats.TypeStr} lv = {Stats.variable}");
        Set_Blessing();
        LocalizeStatsDataUpdater.UpdateLocalizedText();
    }

    /// <summary>
    /// true = enough goods
    /// </summary>
    /// <param name="_piece"></param>
    /// <returns></returns>
    private bool CheckUpgrade(int _piece)
    {
        if (Stats.variable >= Max_level)
        {
            return false;
        }
        return _piece >= GlobalValue.Bless_Inflation * (Stats.variable + 1);
    }

    public void Select(GameObject go)
    {
        if (go.GetComponent<Stats>() == null)
            return;

        Stats = go.GetComponent<Stats>();

        //현재 선택된 축복
        if (Blessing_Btn != null)
        {
            var nav = Blessing_Btn.navigation;
            nav.selectOnRight = go.GetComponent<Button>();
            Blessing_Btn.navigation = nav;
        }

        if (Stats.TypeSprite != null)
        {
            Icon.sprite = Stats.TypeSprite;
        }

        LocalizeStringEvent_Descript.StringReference.SetReference("Stats_Table", Stats.TypeStr + "_Descript");
        LocalizeStatsDataUpdater.SetStats(go.GetComponent<Stats>());
        LocalizeStatsDataUpdater.UpdateLocalizedText();
        Set_Blessing();

    }
}

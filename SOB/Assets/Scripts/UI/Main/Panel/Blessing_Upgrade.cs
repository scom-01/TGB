using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class Blessing_Upgrade : MonoBehaviour
{
    private Stats Stats;
    [SerializeField] private Image Icon;
    [SerializeField] private LocalizeStringEvent LocalizeStringEvent_Descript;
    [SerializeField] private LocalizeStatsDataUpdater LocalizeStatsDataUpdater;

    [SerializeField] private TMP_Text TMP_BlessingCost;
    [SerializeField] private LocalizeStringEvent LocalizeStringEvent__BlessingCost;

    //OnClick
    public void Set(Stats _stats)
    {
        Stats = _stats;
        LocalizeStringEvent_Descript.StringReference.SetReference("Stats_Table", Stats.TypeStr + "_Descript");
        LocalizeStatsDataUpdater.SetStats(_stats);

    }

    private void Set_Blessing()
    {
        var piece = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.hammer_piece;

        // 텍스트 업데이트
        string localizedText = LocalizeStringEvent__BlessingCost.StringReference.GetLocalizedString();
        localizedText += string.Format($" : <color={(piece < 200 * Stats.variable ? "red" : "green")}>{piece}</color> : {200 * Stats.variable}");

        if (TMP_BlessingCost != null)
            TMP_BlessingCost.text = localizedText;
    }
    public void Upgrade()
    {

    }
}

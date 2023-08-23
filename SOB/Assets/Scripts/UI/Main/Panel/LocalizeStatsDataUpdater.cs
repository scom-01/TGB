using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class LocalizeStatsDataUpdater : MonoBehaviour
{
    public TMP_Text localizedTextElement; // UI Text Element with Localize String Event
    public LocalizeStringEvent localizeStringEvent;
    public Stats Stats;

    private void Start()
    {
        // 해당 스크립트 데이터를 이용하여 Localize String Event의 텍스트 값을 변경
        UpdateLocalizedText();
        localizeStringEvent.OnUpdateString.AddListener(delegate { UpdateLocalizedText(); });
    }

    private void UpdateLocalizedText()
    {
        if (Stats == null)
            return;

        // 예시로 데이터를 계산하여 로컬라이징 텍스트 업데이트
        float currentValue = Stats.variable;

        // 텍스트 업데이트
        string localizedText = localizeStringEvent.StringReference.GetLocalizedString();

        localizedText = localizedText.Replace("{variable}", currentValue.ToString());

        localizedTextElement.text = localizedText;        
    }

    public void SetStats(Stats _stats)
    {
        Stats = _stats;
    }
    [ContextMenu("Set Init")]
    public void Init()
    {
        if (localizeStringEvent == null)
            localizeStringEvent = this.GetComponentInChildren<LocalizeStringEvent>();

        if (localizedTextElement == null)
            localizedTextElement = this.GetComponentInChildren<TMP_Text>();
    }
}


using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Unit Unit
    {
        get
        {
            if (unit == null)
            {
                if (isSpriteRender)
                {
                    unit = this.GetComponentInParent<Unit>();                    
                }
                else
                {
                    if (GameManager.Inst?.StageManager != null)
                    {
                        unit = GameManager.Inst.StageManager.player;
                    }
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
                stats = Unit.Core.GetCoreComponent<UnitStats>();
            }
            return stats;
        }
    }
    private UnitStats stats;

    [SerializeField] private bool isSpriteRender = true;
    [SerializeField] private RectTransform healthbarTrasform;
    [SerializeField] private float ZeroPosX;

    private Image Img;
    private TextMeshProUGUI Txt;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Inst?.StageManager == null)
        {
            return;
        }

        if (Stats == null)
        {

            return;
        }

        if (isSpriteRender)
        {
            healthbarTrasform.anchoredPosition = new Vector2(ZeroPosX * (1 - (Stats.CurrentHealth / Stats.StatsData.MaxHealth)), 0);
        }
        else
        {
            Img = this.GetComponent<Image>();
            Txt = this.GetComponentInChildren<TextMeshProUGUI>();
            Img.fillAmount = Stats.CurrentHealth / Stats.StatsData.MaxHealth;
            Txt.text = string.Format($"{(int)Stats.CurrentHealth} / {Stats.StatsData.MaxHealth}");
            if (Img.fillAmount < 0.3f)
            {
                healthbarTrasform.GetComponent<OverlayImg>().SetValue(1 - (Img.fillAmount / 0.3f));
            }
            else
            {
                healthbarTrasform.GetComponent<OverlayImg>().SetValue(0);
            }
        }
    }
}

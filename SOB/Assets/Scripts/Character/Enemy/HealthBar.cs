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
                if (m_isUnit)
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
                stats = Unit.Core.CoreUnitStats;
            }
            return stats;
        }
    }
    private UnitStats stats;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private bool m_isUnit = true;
    [SerializeField] private bool m_IsFollow = true;
    private Image Img;
    [SerializeField] private TextMeshProUGUI Txt;
    private Canvas m_Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = this.GetComponentInParent<Canvas>();
            }
            return _canvas;
        }
    }
    private Canvas _canvas;
    // Update is called once per frame
    void Update()
    {
        if (!m_Canvas.enabled)
            return;

        if (GameManager.Inst?.StageManager == null)
        {
            return;
        }

        if (Stats == null)
        {

            return;
        }

        if (m_Slider != null)
        {
            m_Slider.value = Stats.CurrentHealth / Stats.MaxHealth;

            if(m_IsFollow)
            {
                if (GameManager.Inst.StageManager.Cam != null)
                {
                    m_Slider.transform.position = GameManager.Inst.StageManager.Cam.WorldToScreenPoint(unit.Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0.0f, -0.5f, 0.0f));
                }
                else
                {
                    m_Slider.transform.position = Camera.main.WorldToScreenPoint(unit.Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0.0f, -0.5f, 0.0f));
                }
            }            
        }
        if (Txt != null) 
        {
            Txt.text = string.Format($"{(int)Stats.CurrentHealth} / {Stats.MaxHealth}");        
            Txt = this.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}

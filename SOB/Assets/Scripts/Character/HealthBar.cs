using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
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
    [SerializeField] private BackHealthBarEffect HealthBarEffect
    {
        get
        {
            if(m_HealthBarEffect == null)
            {
                m_HealthBarEffect = this.GetComponentInChildren<BackHealthBarEffect>();
            }
            return m_HealthBarEffect;
        }
    }
    private BackHealthBarEffect m_HealthBarEffect;
    [SerializeField] private float lerpduration = 0.5f;
    private Coroutine runningCoroutine;

    private void OnEnable()
    {
        if (Stats != null)
        {
            Stats.OnChangeHealth -= UpdateBar;
            Stats.OnChangeHealth += UpdateBar;
        }
    }
    private void OnDisable()
    {
        if (Stats != null)
        {
            Stats.OnChangeHealth -= UpdateBar;
        }
    }

    private void UpdateBar()
    {
        // 슬라이더의 목표 값을 계산합니다.
        float targetValue = Stats.CurrentHealth / Stats.MaxHealth;


        if (runningCoroutine != null) 
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(LerpHealthBar(targetValue));
        if (Txt != null)
        {
            Txt.text = string.Format($"{(int)Stats.CurrentHealth} / {Stats.MaxHealth}");
        }                
    }
    IEnumerator LerpHealthBar(float targetValue)
    {
        if (m_Slider != null)
        {
            float lerpValue = m_Slider.value;
            float startTime = Time.time;

            while (lerpValue > targetValue)
            {
                float elapsedTime = Time.time - startTime;
                lerpValue = Mathf.Lerp(m_Slider.value, targetValue, elapsedTime / lerpduration);
                m_Slider.value = lerpValue;

                if (m_IsFollow)
                {
                    Camera mainCamera = Camera.main; // 필요한 경우에만 Camera 변수를 사용합니다.
                    if (GameManager.Inst.StageManager.Cam != null)
                    {
                        mainCamera = GameManager.Inst.StageManager.Cam;
                    }

                    m_Slider.transform.position = mainCamera.WorldToScreenPoint(unit.Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0.0f, -0.5f, 0.0f));
                }

                yield return null;
            }

            CallBackHealthBarEffect();
        }
    }

    void CallBackHealthBarEffect()
    {
        Debug.Log("Success UpdateHealthBar");
        if (HealthBarEffect != null)
            HealthBarEffect.Health_value = m_Slider.value;
    }

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
    //void Update()
    //{
    //    if (!m_Canvas.enabled)
    //        return;

    //    if (GameManager.Inst?.StageManager == null)
    //    {
    //        return;
    //    }

    //    if (Stats == null)
    //    {

    //        return;
    //    }

    //    if (m_Slider != null)
    //    {
    //        m_Slider.value = Stats.CurrentHealth / Stats.MaxHealth;

    //        if(m_IsFollow)
    //        {
    //            if (GameManager.Inst.StageManager.Cam != null)
    //            {
    //                m_Slider.transform.position = GameManager.Inst.StageManager.Cam.WorldToScreenPoint(unit.Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0.0f, -0.5f, 0.0f));
    //            }
    //            else
    //            {
    //                m_Slider.transform.position = Camera.main.WorldToScreenPoint(unit.Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0.0f, -0.5f, 0.0f));
    //            }
    //        }            
    //    }
    //    if (Txt != null) 
    //    {
    //        Txt.text = string.Format($"{(int)Stats.CurrentHealth} / {Stats.MaxHealth}");        
    //        Txt = this.GetComponentInChildren<TextMeshProUGUI>();
    //    }
    //}
}

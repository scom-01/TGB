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
                unit = this.GetComponentInParent<Unit>();
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
    [SerializeField] private bool m_IsFollow = true;
    [SerializeField] private bool m_IsShowTxt = true;
    [SerializeField] private TextMeshProUGUI Txt;
    [SerializeField]
    private Camera m_Camera;
    private BackHealthBarEffect HealthBarEffect
    {
        get
        {
            if (m_HealthBarEffect == null)
            {
                m_HealthBarEffect = this.GetComponentInChildren<BackHealthBarEffect>();
            }
            return m_HealthBarEffect;
        }
    }
    private BackHealthBarEffect m_HealthBarEffect;
    [SerializeField] private float lerpduration = 0.5f;
    private Coroutine runningCoroutine;

    private void Start()
    {
        m_Camera = Camera.main; // 필요한 경우에만 Camera 변수를 사용합니다.
        if (GameManager.Inst.StageManager.Cam != null)
        {
            m_Camera = GameManager.Inst.StageManager.Cam;
        }
    }
    private void FixedUpdate()
    {
        if (!m_IsFollow)
            return;

        m_Slider.transform.position = m_Camera.WorldToScreenPoint(unit.Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0.0f, -0.5f, 0.0f));        
    }
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
            if (m_IsShowTxt)
            {
                Txt.gameObject.SetActive(true);
                Txt.text = string.Format($"{(int)Stats.CurrentHealth} / {Stats.MaxHealth}");
            }
            else
            {
                Txt.gameObject.SetActive(false);
            }
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
}

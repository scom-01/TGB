using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI HitTextMeshPro
    {
        get
        {
            if (hitTextMeshPro == null)
            {
                hitTextMeshPro = this.GetComponent<TextMeshProUGUI>();
            }
            return hitTextMeshPro;
        }
        set => hitTextMeshPro = value;
    }

    private TextMeshProUGUI hitTextMeshPro;

    public float DamageAmount
    {
        get
        {
            return damageAmount;
        }
        set
        {
            damageAmount = value;
            if (HitTextMeshPro != null)
            {
                HitTextMeshPro.text = string.Format("{0:#,###}", damageAmount);
            }
            else
            {
                Debug.LogWarning("HitTextMeshPro is null");
            }
        }
    }
    public float FontSize
    {
        get
        {
            return fontSize;
        }
        set
        {
            fontSize = value;
            if (HitTextMeshPro != null)
            {
                HitTextMeshPro.fontSize = fontSize;
            }
            else
            {
                Debug.LogWarning("HitTextMeshPro is null");
            }
        }
    }
    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
            if (HitTextMeshPro != null)
            {
                HitTextMeshPro.color = color;
            }
            else
            {
                Debug.LogWarning("HitTextMeshPro is null");
            }
        }
    }

    private float damageAmount;
    private float fontSize;
    private Color color;

    private RectTransform CoverTransform
    {
        get
        {
            if(m_CoverTransform == null)
            {
                m_CoverTransform = this.GetComponentInParent<DamageTextParent>().GetComponent<RectTransform>();
            }
            return m_CoverTransform;
        }
    }
    private RectTransform m_CoverTransform;
    private DmgTxtPooling Parent
    {
        get
        {
            if (parent == null)
            {
                parent = this.GetComponentInParent<DmgTxtPooling>();
            }
            return parent;
        }
    }
    private DmgTxtPooling parent;
    private void Awake()
    {
        this.HitTextMeshPro = this.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// DamageText세팅
    /// </summary>
    /// <param name="damage">데미지크기</param>
    /// <param name="fontsize">폰트 사이즈</param>
    /// <param name="color">폰트 색상</param>
    public void SetText(float damage, float fontsize, Color color)
    {
        this.DamageAmount = damage;
        this.FontSize = fontsize;
        this.Color = color;
    }

    /// <summary>
    /// AnimationEvent
    /// </summary>
    public void FinishAnim()
    {
        if (Parent != null)
        {
            Parent.ReturnObject(CoverTransform.gameObject);
        }
    }
}

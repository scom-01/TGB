using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI HitTextMeshPro { get => hitTextMeshPro; set => hitTextMeshPro = value; }
    
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
            if(HitTextMeshPro != null)
            {
                HitTextMeshPro.text = damageAmount.ToString();
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

    private void Awake()
    {
        this.HitTextMeshPro = this.GetComponent<TextMeshProUGUI>();
    }

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
        Destroy(this.transform.parent.gameObject);
    }
}

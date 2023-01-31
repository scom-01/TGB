using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI HitTextMeshPro { get => hitTextMeshPro; set => hitTextMeshPro = value; }
    
    private TextMeshProUGUI hitTextMeshPro;

    public float DamageAmount { get => damageAmount; set => damageAmount = value; }
    public float FontSize { get => fontSize; set => fontSize = value; }
    public Color Color { get => color; set => color = value; }


    private float damageAmount;
    private float fontSize;
    private Color color;

    private void Awake()
    {
        this.HitTextMeshPro = this.GetComponent<TextMeshProUGUI>();
        if (HitTextMeshPro != null)
        {
            HitTextMeshPro.text = DamageAmount.ToString();
        }
    }

    public void ChangeFontColor()
    {
        HitTextMeshPro.color = Color;
    }

    /// <summary>
    /// AnimationEvent
    /// </summary>
    private void FinishAnim()
    {
        Destroy(this.gameObject);
    }
}

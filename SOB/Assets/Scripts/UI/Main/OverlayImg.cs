using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class OverlayImg : MonoBehaviour
{
    private Image Img
    {
        get
        {
            if (img == null)
            {
                img = this.GetComponent<Image>();
            }
            return img;
        }
    }
    private Image img;

    [SerializeField] private Sprite Sprite;
    [Range(0, 1f)] public float value = 0;
    [Range(0, 1f)] private float oldvalue = 0;

    // Update is called once per frame
    void Update()
    {
        if (oldvalue == value)
            return;

        if (Img != null) 
        {
            Color tempColor = new Color(1, 1, 1, value);
            Img.color = tempColor;
            oldvalue = value;
        }
    }

    public void SetValue(float _value)
    {
        value = _value;
    }
}

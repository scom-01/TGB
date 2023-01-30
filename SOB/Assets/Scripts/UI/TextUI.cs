using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField]
    private Texture Backtexture2D;
    [SerializeField]
    private Texture texture2D;

    [SerializeField]
    private string text;
    [SerializeField]
    private float fontSize;
    [SerializeField]
    private float size;
    [SerializeField]
    private float padding;
    // Start is called before the first frame update
    void Start()
    {
        var rawImage = GetComponentsInChildren<RawImage>();

        rawImage[0].texture = Backtexture2D;
        //rawImage[0].rectTransform.sizeDelta = new Vector2(size, size);

        rawImage[1].texture = texture2D;
        //rawImage[1].rectTransform.sizeDelta = new Vector2(size - padding, size - padding);

        GetComponentInChildren<TextMeshProUGUI>().text = text;
        GetComponentInChildren<TextMeshProUGUI>().fontSize = fontSize;
    }
}

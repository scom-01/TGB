using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField]
    private Sprite Backsprite;
    [SerializeField]
    private Sprite sprite;

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
        var Image = GetComponentsInChildren<Image>();

        Image[0].sprite = Backsprite;
        //rawImage[0].rectTransform.sizeDelta = new Vector2(size, size);

        Image[1].sprite = sprite;
        //rawImage[1].rectTransform.sizeDelta = new Vector2(size - padding, size - padding);

        GetComponentInChildren<TextMeshProUGUI>().text = text;
        GetComponentInChildren<TextMeshProUGUI>().fontSize = fontSize;
    }
}

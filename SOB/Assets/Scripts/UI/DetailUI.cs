using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DetailUI : MonoBehaviour
{
    [Header("---Main---")]
    [SerializeField]
    private GameObject mainUI;
    [SerializeField] private LocalizeStringEvent MainStringEvent;

    [Tooltip("하위 컴포넌트 중 'MainText' Text String")]
    public string ItemName { get; private set; }

    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }
            return canvas;
        }
    }
    private Canvas canvas;

    [SerializeField] private Image Icon;

    [Header("---Sub---")]    
    [SerializeField]
    private GameObject subUI;
    [SerializeField] private LocalizeStringEvent SubStringEvent;
    [Tooltip("하위 컴포넌트 중 'SubText' Text String")]

    public void SetInit(string _ItemName,Sprite _sprite)
    {
        if (_ItemName != "") 
        {
            ItemName = _ItemName;

            if (MainStringEvent != null)
            {
                MainStringEvent.StringReference.SetReference("Item_Table", ItemName);
            }

            if (SubStringEvent != null)
            {
                SubStringEvent.StringReference.SetReference("Item_Table", ItemName + "_Descript");
            }
        }

        if(Icon!=null)
        {
            Icon.sprite = _sprite;
        }
    }
}
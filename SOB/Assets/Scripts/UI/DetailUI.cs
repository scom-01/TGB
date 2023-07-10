using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DetailUI : MonoBehaviour
{
    [Header("---Main---")]
    [SerializeField]
    private GameObject mainUI;
    [SerializeField] private LocalizeStringEvent MainStringEvent;

    [Tooltip("하위 컴포넌트 중 'MainText' Text String")]
    public string ItemName 
    { 
        get 
        {
            return mainItemName; 
        }
        set
        {
            mainItemName = value;
            if (MainStringEvent != null)
            {
                MainStringEvent.StringReference.SetReference("Item_Table", mainItemName);
            }
        }
    }
    private string mainItemName;

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

    public Image Icon;

    [Header("---Sub---")]    
    [SerializeField]
    private GameObject subUI;
    [SerializeField] private LocalizeStringEvent SubStringEvent;
    [Tooltip("하위 컴포넌트 중 'SubText' Text String")]
    public string ItemDescript
    {
        get
        {
            return itemDescript;
        }
        set
        {
            itemDescript = value;
            if (SubStringEvent != null)
            {
                SubStringEvent.StringReference.SetReference("Item_Table", ItemName + "_Descript");
            }
        }
    }
    private string itemDescript;

    //ContentSizeFitter csf;
    //private void Awake()
    //{
    //    csf = GetComponent<ContentSizeFitter>();
    //    //this.gameObject.SetActive(false);
    //}

    //private void OnEnable()
    //{
    //    //처음 SetActive시 ContentSizeFitter가 먹히지않던 해결 코드
    //    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
    //}
}
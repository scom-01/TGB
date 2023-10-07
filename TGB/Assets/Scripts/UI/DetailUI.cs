using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DetailUI : MonoBehaviour
{
    [Header("---Main---")]
    [SerializeField]
    private GameObject mainUI;
    [SerializeField] private LocalizeStringEvent MainStringEvent;
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
    [SerializeField] private TextMeshProUGUI StatsDescript;
    [Tooltip("하위 컴포넌트 중 'SubText' Text String")]

    public void SetInit(LocalizedString _ItemNameLocal, LocalizedString _ItemDescriptLocal, Sprite _sprite, string _StatsDescripts)
    {
        if (_ItemNameLocal != null) 
        {
            MainStringEvent.StringReference.SetReference("Item_Table", _ItemNameLocal.TableEntryReference);
        }

        if (SubStringEvent != null)
        {
            SubStringEvent.StringReference.SetReference("Item_Table", _ItemDescriptLocal.TableEntryReference);
        }

        if (StatsDescript != null)
        {
            StatsDescript.text = "";
            StatsDescript.text = _StatsDescripts;
        }

        if (Icon!=null)
        {
            Icon.sprite = _sprite;
        }
    }
}
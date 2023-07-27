using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class SceneNameFade : MonoBehaviour
{
    public TextMeshProUGUI FadeString;
    public LocalizeStringEvent LocalizeStringEvent;
    public LocalizedString localizedStr;
    // Start is called before the first frame update
    void Start()
    {
        if(LocalizeStringEvent != null)
            LocalizeStringEvent.StringReference = localizedStr;
    }
}
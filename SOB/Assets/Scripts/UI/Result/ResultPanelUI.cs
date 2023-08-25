using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelUI : MonoBehaviour
{
    [Header("ResultItem")]
    [SerializeField] private List<ResultItem> ResultItemList;

    public void UpdateResultPanel()
    {
        foreach(var item in ResultItemList)
        {
            item.Init();
        }
    }
}


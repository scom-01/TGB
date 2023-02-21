using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public StatsItemSO StatsItemData
    {
        get => statsItemData;
        set
        {
            if(value == null)
            {

            }
            statsItemData = value;
            Init();
        }
    }

    [SerializeField] private StatsItemSO statsItemData;
    public bool isSelect
    {
        set => BackImgAlpha = value ? 1f : 0.5f;
    }

    public Image iconBackImg;
    public Image iconImg;

    private float BackImgAlpha
    {
        set => iconBackImg.color = new Color(iconBackImg.color.r, iconBackImg.color.g, iconBackImg.color.b, value);
    }

    public void Init()
    {
        if (statsItemData != null)
        {
            iconImg.sprite = statsItemData.ItemSprite;
            iconImg.color = new Color(iconImg.color.r, iconImg.color.g, iconImg.color.b, 1f);
        }
        else
        {
            iconImg.sprite = null;
            iconImg.color = new Color(iconImg.color.r, iconImg.color.g, iconImg.color.b, 0);
        }
    }
}


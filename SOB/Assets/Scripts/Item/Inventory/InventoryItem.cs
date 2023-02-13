using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [field: SerializeField] public StatsItemSO StatsItemData;
    public bool isSelect
    {
        set => BackImgAlpha = value ? 1f : 0.75f;
    }

    public Image iconBackImg;
    public Image iconImg;
    private float BackImgAlpha
    {
        set => iconBackImg.color = new Color(iconBackImg.color.r, iconBackImg.color.g, iconBackImg.color.b, value);
    }

    public void Init()
    {
        iconImg.sprite = StatsItemData.ItemSprite;
    }
}


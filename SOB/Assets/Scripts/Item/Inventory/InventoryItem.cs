using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, ISelectHandler
{
    /// <summary>
    /// 인벤토리 Data
    /// </summary>
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
    public bool isSelect;
    //{
    //    set => BackImgAlpha = value ? 1f : 0.5f;
    //}

    public int Index;

    public Image iconBackImg;
    /// <summary>
    /// 아이템 아이콘 이미지
    /// </summary>
    public Image iconImg;

    private float BackImgAlpha
    {
        set => iconBackImg.color = new Color(iconBackImg.color.r, iconBackImg.color.g, iconBackImg.color.b, value);
    }
    
    public void CurrentItem()
    {
        if(this.GetComponentInParent<InventoryItems>() ==null)
        {
            return;
        }

        this.GetComponentInParent<InventoryItems>().CurrentSelectItemIndex = Index;
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

    //OnSelect
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if(eventData.selectedObject == this.gameObject)
        {
            Debug.Log($"{this.gameObject.name} selected");
            CurrentItem();
        }
    }
}


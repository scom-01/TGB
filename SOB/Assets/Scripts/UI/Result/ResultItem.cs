using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ResultItem : MonoBehaviour
{
    [SerializeField] private StatsItemSO curritem;
    [SerializeField] private StatsItemSO olditem;

    [SerializeField] private Image Img;
    [SerializeField] private Image BackImg;

    [SerializeField] private int index;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Inst == null)
        {
            return;
        }    

        curritem = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.Items[index].StatsItemData;

        if(curritem == null)
        {
            Img.enabled = false;
            BackImg.enabled = false;
            return;
        }
        else
        {
            Img.enabled = true;
            BackImg.enabled = true;
        }

        if (olditem != curritem)
        {
            Img.sprite = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.Items[index].iconImg.sprite;
            BackImg.sprite = GameManager.Inst.SubUI.InventorySubUI.InventoryItems.Items[index].iconBackImg.sprite;
            olditem = curritem;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EffectTextUI : MonoBehaviour
{
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

    private Image BackImg
    { 
        get
        {
            if (backImg == null) 
            {
                backImg = this.GetComponentInChildren<Image>();
            }
            return backImg;
        }
    }
    private Image backImg = null;

    private Animator Anim
    {
        get
        {
            if (anim == null) 
            {
                anim = this.GetComponent<Animator>();
            }
            return anim;
        }
    }
    private Animator anim;
    public string ItemName;
    public LocalizeStringEvent EffectTextStringEvent;

    [ContextMenu("Set EffectText")]
    public void TestSet()
    {
        EffectTextStringEvent.StringReference.Add("itemName", new LocalizedString("Item_Table", ItemName));
        //Local Variable만 변경되면 Refresh되지않기에 Refresh로 업데이트
        EffectTextStringEvent.RefreshString();
    }

    public void EffectTextOn()
    {
        Canvas.enabled = true;
        SetEffectText(ItemName);
    }

    public void SetEffectText(string StringTableKey)
    {
        if (EffectTextStringEvent != null && StringTableKey != "")
        {
            EffectTextStringEvent.StringReference.Add("itemName", new LocalizedString("Item_Table", StringTableKey));
            
            //Local Variable만 변경되면 Refresh되지않기에 Refresh로 업데이트
            EffectTextStringEvent.RefreshString();
            Anim.SetBool("Action", true);
        }
    }public void SetEffectText(Sprite _sprite, string StringTableKey)
    {
        if (BackImg != null && _sprite != null) 
        {
            BackImg.sprite = _sprite;
        }

        if (EffectTextStringEvent != null && StringTableKey != "")
        {
            EffectTextStringEvent.StringReference.Add("itemName", new LocalizedString("Item_Table", StringTableKey));

            //Local Variable만 변경되면 Refresh되지않기에 Refresh로 업데이트
            EffectTextStringEvent.RefreshString();
            Anim.SetBool("Action", true);
        }
    }

    public void SetActionfalse()
    {
        Anim.SetBool("Action", false);
        Canvas.enabled = false;
    }
}

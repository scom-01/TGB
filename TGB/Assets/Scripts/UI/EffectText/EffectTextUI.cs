using SCOM.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
public class EffectTextUI : UIManager
{
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

    public LocalizedString ItemNameLocal;
    public LocalizeStringEvent EffectTextStringEvent;

    public List<LocalizedString> UnlockitemNames;

    [ContextMenu("Set EffectText")]
    public void TestSet()
    {
        EffectTextStringEvent.StringReference.Add("itemName", new LocalizedString("Item_Table", ItemNameLocal.TableEntryReference));
        //Local Variable만 변경되면 Refresh되지않기에 Refresh로 업데이트
        EffectTextStringEvent.RefreshString();
    }

    [ContextMenu("Add EffectText")]
    public void AddItem()
    {
        UnlockitemNames.Add(ItemNameLocal);
    }

    public void EffectTextOn()
    {
        StartCoroutine(EffectCoroutine());
    }

    public void SetEffectText(LocalizedString StringTableKey)
    {
        if (EffectTextStringEvent != null && StringTableKey != null)
        {
            EffectTextStringEvent.StringReference.Add("itemName", new LocalizedString("Item_Table", StringTableKey.TableEntryReference));
            
            //Local Variable만 변경되면 Refresh되지않기에 Refresh로 업데이트
            EffectTextStringEvent.RefreshString();
            animator.SetBool("Action", true);
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
            animator.SetBool("Action", true);
        }
    }

    public void SetActionfalse()
    {
        animator.SetBool("Action", false);
        Canvas.enabled = false;
    }

    IEnumerator EffectCoroutine()
    {
        for (int i = 0; i < UnlockitemNames.Count; i++) 
        {
            Canvas.enabled = true;
            SetEffectText(UnlockitemNames[i]);
            while(animator.GetBool("Action"))
            {
                yield return null;
            }            
        }
        UnlockitemNames.Clear();
        yield return null;
    }
}

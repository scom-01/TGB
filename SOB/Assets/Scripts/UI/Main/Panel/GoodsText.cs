using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodsText : MonoBehaviour
{
    [SerializeField] private GOODS_TPYE Type;

    private Sprite[] SpriteAtlas
    {
        get
        {
            if (spriteAtlas.Length == 0)
            {
                spriteAtlas = Resources.LoadAll<Sprite>(GlobalValue.Icon_UI_Path);
            }
            return spriteAtlas;
        }
    }

    private Sprite[] spriteAtlas = { };
    private Sprite GoldIcon
    {
        get
        {
            if (goldIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Icon_Gold_Path)
                    {
                        goldIcon = sprite;
                        break;
                    }
                }
            }
            return goldIcon;
        }
    }
    private Sprite goldIcon;
    private Sprite FireIcon
    {
        get
        {
            if (fireIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Icon_FireGoods_Path)
                    {
                        fireIcon = sprite;
                        break;
                    }
                }
            }
            return fireIcon;
        }
    }
    private Sprite fireIcon;
    private Sprite WaterIcon
    {
        get
        {
            if (waterIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Icon_WaterGoods_Path)
                    {
                        waterIcon = sprite;
                        break;
                    }
                }
            }
            return waterIcon;
        }
    }
    private Sprite waterIcon;
    private Sprite EarthIcon
    {
        get
        {
            if (earthIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Icon_EarthGoods_Path)
                    {
                        earthIcon = sprite;
                        break;
                    }
                }
            }
            return earthIcon;
        }
    }
    private Sprite earthIcon;
    private Sprite WindIcon
    {
        get
        {
            if (windIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Icon_WindGoods_Path)
                    {
                        windIcon = sprite;
                        break;
                    }
                }
            }
            return windIcon;
        }
    }
    private Sprite windIcon;

    private Sprite TypeIcon
    { 
        get
        {
            Sprite img = null;
            switch (Type)
            {
                case GOODS_TPYE.Gold:
                    img = GoldIcon;
                    return img;
                case GOODS_TPYE.FireGoods:
                    img = FireIcon;
                    return img;
                case GOODS_TPYE.WaterGoods:
                    img = WaterIcon;
                    return img;
                case GOODS_TPYE.EarthGoods:
                    img = EarthIcon;
                    return img;
                case GOODS_TPYE.WindGoods:
                    img = WindIcon;
                    return img;
            }
            return img;
        }
    }
    private int TypeGoodsCount
    {
        get
        {
            int count = 0;
            if(DataManager.Inst != null)
            {
                switch (Type)
                {
                    case GOODS_TPYE.Gold:
                        count = DataManager.Inst.GoldCount;
                        return count;
                    case GOODS_TPYE.FireGoods:
                        count = DataManager.Inst.ElementalGoodsCount.FireGoods;
                        return count;
                    case GOODS_TPYE.WaterGoods:
                        count = DataManager.Inst.ElementalGoodsCount.WaterGoods;
                        return count;
                    case GOODS_TPYE.EarthGoods:
                        count = DataManager.Inst.ElementalGoodsCount.EarthGoods;
                        return count;
                    case GOODS_TPYE.WindGoods:
                        count = DataManager.Inst.ElementalGoodsCount.WindGoods;
                        return count;
                }
            }
            return count;
        }
    }

    [SerializeField] private Image IconImg;
    [SerializeField] private TextMeshProUGUI CurrGoodsCountTxt;
    [SerializeField] private bool Showcolon;
    [SerializeField] private bool isLeftcolon;
    private int oldGoodsCount = -1;

    // Update is called once per frame
    void Update()
    {
        if (IconImg != null && TypeIcon != null)
        {
            IconImg.sprite = TypeIcon;
        }

        if(oldGoodsCount != TypeGoodsCount)
        {
            if (CurrGoodsCountTxt != null)
            {
                CurrGoodsCountTxt.text = isLeftcolon ? ((Showcolon ? " : " : "") + TypeGoodsCount.ToString()) : (TypeGoodsCount.ToString() + (Showcolon ? " : " : ""));
            }
            oldGoodsCount = TypeGoodsCount;
        }
    }
}

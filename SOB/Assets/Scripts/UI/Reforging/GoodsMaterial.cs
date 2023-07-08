using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class GoodsMaterial : MonoBehaviour
{
    public GOODS_TPYE Type;

    private Sprite[] SpriteAtlas
    {
        get
        {
            if (spriteAtlas.Length == 0)
            {
                spriteAtlas = Resources.LoadAll<Sprite>(GlobalValue.Sprites_UI_Path + "/Symbol_Sheet");
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
                    if (sprite.name == GlobalValue.Symbol_Gold_Path)
                    {
                        goldIcon = sprite;
                        return goldIcon;
                    }
                }
                goldIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Gold_Path);
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
                    if (sprite.name == GlobalValue.Symbol_Fire_Path)
                    {
                        fireIcon = sprite;
                        return fireIcon;
                    }
                }
                fireIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Fire_Path);
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
                    if (sprite.name == GlobalValue.Symbol_Water_Path)
                    {
                        waterIcon = sprite;
                        return waterIcon;
                    }
                }
                waterIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Water_Path);
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
                    if (sprite.name == GlobalValue.Symbol_Earth_Path)
                    {
                        earthIcon = sprite;
                        return earthIcon;
                    }
                }
                earthIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Earth_Path);
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
                    if (sprite.name == GlobalValue.Symbol_Wind_Path)
                    {
                        windIcon = sprite;
                        return windIcon;
                    }
                }
                windIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Wind_Path);
            }
            return windIcon;
        }
    }
    private Sprite windIcon;

    [SerializeField] private Image currImg;
    [SerializeField] private Image reforgingImg;
    [SerializeField] private TextMeshProUGUI CurrGoodsCountTxt;
    [SerializeField] private TextMeshProUGUI CostGoodsCountTxt;
    private int CurrGoodsCount = -1;
    [HideInInspector] public int CostGoodsCount = -1;
    [SerializeField] private Color DefaultColor;
    [SerializeField] private Color ShortageColor;
    [SerializeField] private Color enoughColor;

    public bool CanReforging => CurrGoodsCount < CostGoodsCount ? false : true;
    [ContextMenu("Update GoodsMaterial")]
    private void Test()
    {
        UpdateGoodsMaterial();
    }
    public void UpdateGoodsMaterial(GOODS_TPYE type = GOODS_TPYE.Gold, int curr = 0, int cost = 0)
    {
        Type = type;
        CurrGoodsCount = curr;
        CostGoodsCount = cost;

        Sprite img = null;
        switch (Type)
        {
            case GOODS_TPYE.Gold:
                img = GoldIcon;
                break;
            case GOODS_TPYE.FireGoods:
                img = FireIcon;
                break;
            case GOODS_TPYE.WaterGoods:
                img = WaterIcon;
                break;
            case GOODS_TPYE.EarthGoods:
                img = EarthIcon;
                break;
            case GOODS_TPYE.WindGoods:
                img = WindIcon;
                break;

        }
        if (img != null)
        {
            if (currImg != null)
            {
                currImg.sprite = img;
            }
            if (reforgingImg != null)
            {
                reforgingImg.sprite = img;
            }
        }
        
        if (CurrGoodsCountTxt != null)
        {
            CurrGoodsCountTxt.color = DefaultColor;
            CurrGoodsCountTxt.text = CurrGoodsCount <= 0 ? "0" : string.Format("{0:#,###}", CurrGoodsCount);
        }

        if (CostGoodsCountTxt != null)
        {
            CostGoodsCountTxt.color = curr < cost ? ShortageColor : enoughColor;
            CostGoodsCountTxt.text = CostGoodsCount <= 0 ? "0" : string.Format("{0:#,###}", CostGoodsCount);
        }
    }
}
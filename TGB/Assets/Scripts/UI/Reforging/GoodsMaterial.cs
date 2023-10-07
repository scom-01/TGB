using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class GoodsMaterial : MonoBehaviour
{
    public GOODS_TPYE Type;

    private SpriteAtlas SpriteAtlas
    {
        get
        {
            if (m_SpriteAtlas == null)
            {
                m_SpriteAtlas = Resources.Load<SpriteAtlas>(GlobalValue.Sprites_UI_Path + "/Symbol_Atlas");
            }
            return m_SpriteAtlas;
        }
    }
    private SpriteAtlas m_SpriteAtlas;
    private Sprite GoldIcon
    {
        get
        {
            if (goldIcon == null)
            {
                goldIcon = SpriteAtlas.GetSprite(GlobalValue.Symbol_Gold_Path);
                if (goldIcon == null)
                {
                    goldIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Gold_Path);
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
                fireIcon = SpriteAtlas.GetSprite(GlobalValue.Symbol_Fire_Path);
                if (fireIcon == null)
                {
                    fireIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Fire_Path);
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
                waterIcon = SpriteAtlas.GetSprite(GlobalValue.Symbol_Water_Path);
                if (waterIcon == null)
                {
                    waterIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Water_Path);
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
                earthIcon = SpriteAtlas.GetSprite(GlobalValue.Symbol_Earth_Path);
                if (earthIcon == null)
                {
                    earthIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Earth_Path);
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
                windIcon = SpriteAtlas.GetSprite(GlobalValue.Symbol_Wind_Path);
                if (windIcon == null)
                {
                    windIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Wind_Path); ;
                }
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
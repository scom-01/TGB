using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class GoodsText : MonoBehaviour
{
    [SerializeField] private GOODS_TPYE Type;
    private GOODS_TPYE OldType = GOODS_TPYE.None;

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
                        count = DataManager.Inst.JSON_DataParsing.m_JSON_Goods.gold;
                        return count;
                    case GOODS_TPYE.FireGoods:
                        count = DataManager.Inst.JSON_DataParsing.m_JSON_Goods.elementalGoods.FireGoods;
                        return count;
                    case GOODS_TPYE.WaterGoods:
                        count = DataManager.Inst.JSON_DataParsing.m_JSON_Goods.elementalGoods.WaterGoods;
                        return count;
                    case GOODS_TPYE.EarthGoods:
                        count = DataManager.Inst.JSON_DataParsing.m_JSON_Goods.elementalGoods.EarthGoods;
                        return count;
                    case GOODS_TPYE.WindGoods:
                        count = DataManager.Inst.JSON_DataParsing.m_JSON_Goods.elementalGoods.WindGoods;
                        return count;
                }
            }
            return count;
        }
    }

    [SerializeField] private Image IconImg;
    [SerializeField] private LocalizeStringEvent CurrGoodsTxtLocalStringEvent;
    [SerializeField] private TextMeshProUGUI CurrGoodsCountTxt;
    [SerializeField] private bool Showcolon;
    [SerializeField] private bool isLeftcolon;
    private int oldGoodsCount = -1;


    private Canvas m_Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = this.GetComponentInParent<Canvas>();
            }
            return _canvas;
        }
    }
    private Canvas _canvas;
    // Update is called once per frame
    void Update()
    {
        if (!m_Canvas.enabled)
        {
            return;
        }

        if (IconImg != null && TypeIcon != null)
        {
            if(OldType != Type)
            {
                IconImg.sprite = TypeIcon;
                OldType = Type;
            }
        }

        if(CurrGoodsTxtLocalStringEvent != null)
        {
            CurrGoodsTxtLocalStringEvent.StringReference.SetReference("Goods_Table", Type.ToString());
        }

        if(oldGoodsCount != TypeGoodsCount)
        {
            if (CurrGoodsCountTxt != null)
            {
                CurrGoodsCountTxt.text = isLeftcolon ? ((Showcolon ? " : " : "") + string.Format("{0:#,##0}", TypeGoodsCount)) : (string.Format("{0:#,##0}", TypeGoodsCount) + (Showcolon ? " : " : ""));
            }
            oldGoodsCount = TypeGoodsCount;
        }
    }
}

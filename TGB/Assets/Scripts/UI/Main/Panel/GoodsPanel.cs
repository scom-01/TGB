using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GoldCountText;
    [SerializeField] private TextMeshProUGUI ShardsCountText;
    [SerializeField] private TextMeshProUGUI FireGoodsCountText;
    [SerializeField] private TextMeshProUGUI WaterGoodsCountText;
    [SerializeField] private TextMeshProUGUI EarthGoodsCountText;
    [SerializeField] private TextMeshProUGUI WindGoodsCountText;

    private int currentGold;
    private int currentShards;
    private Goods_Data currentElementalGoods;
    
    private int oldGold = 0;
    private int oldShards = 0;
    private Goods_Data oldElementalGoods = new Goods_Data();

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Inst != null)
        {
            currentElementalGoods = DataManager.Inst.GameGoodsLoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.Inst != null)
        {
            currentElementalGoods = DataManager.Inst.GameGoodsLoad();
        }

        if(oldGold != currentGold)
        {
            if (GoldCountText != null)
            {
                GoldCountText.text = " : " + currentGold.ToString();
            }
            oldGold = currentGold;
        }

        if(oldShards != currentShards)
        {
            if (ShardsCountText != null)
            {
                ShardsCountText.text = " : " + currentShards.ToString();
            }
            oldShards = currentShards;
        }        

        if(oldElementalGoods != currentElementalGoods)
        {
            if (FireGoodsCountText != null)
            {
                if (currentElementalGoods.FireGoods <= 0)
                {
                    FireGoodsCountText.gameObject.SetActive(false);
                }
                else
                {
                    FireGoodsCountText.gameObject.SetActive(true);
                    FireGoodsCountText.text = " : " + currentElementalGoods.FireGoods.ToString();
                }
            }

            if (WaterGoodsCountText != null)
            {
                if (currentElementalGoods.WaterGoods <= 0)
                {
                    WaterGoodsCountText.gameObject.SetActive(false);
                }
                else
                {
                    WaterGoodsCountText.gameObject.SetActive(true);
                    WaterGoodsCountText.text = " : " + currentElementalGoods.WaterGoods.ToString();
                }
            }

            if (EarthGoodsCountText != null)
            {
                if (currentElementalGoods.EarthGoods <= 0)
                {
                    EarthGoodsCountText.gameObject.SetActive(false);
                }
                else
                {
                    EarthGoodsCountText.gameObject.SetActive(true);
                    EarthGoodsCountText.text = " : " + currentElementalGoods.EarthGoods.ToString();
                }
            }

            if (WindGoodsCountText != null)
            {
                if (currentElementalGoods.WindGoods <= 0)
                {
                    WindGoodsCountText.gameObject.SetActive(false);
                }
                else
                {
                    WindGoodsCountText.gameObject.SetActive(true);
                    WindGoodsCountText.text = " : " + currentElementalGoods.WindGoods.ToString();
                }
            }

            oldElementalGoods = currentElementalGoods;
        }
        
    }
}

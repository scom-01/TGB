using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoodsItem : MonoBehaviour
{
    [Tooltip("Goods Type")]
    public GOODS_TPYE Goods;
    public int Amount;
    public int DropCount;
    public GameObject GoodsPrefab;
    public Sprite GoodsSprite;
    public AudioClip EquipSoundClip;
    public float CircleSize;
    public float DestroyTime;


    private void OnDisable()
    {
        DropGoods();
    }

    public void DropGoods()
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
            return;

        for (int i = 0; i < DropCount; i++)
        {
            var goods = Instantiate(GoodsPrefab, GameManager.Inst.StageManager.IM.transform);
            goods.transform.position = gameObject.transform.position;
            var goodsData =  goods.GetComponent<GoodsData>();
            goodsData.SR.sprite = GoodsSprite;
            goodsData.CircleSize = CircleSize;
            goodsData.RB2D.AddForce(Vector2.up);
            goodsData.Goods = Goods;
            goodsData.Amount = Amount;
            goodsData.EquipSoundClip = EquipSoundClip;
            goodsData.InvokeTime = DestroyTime;    //DestroyTime
            goodsData.isInit = true;
        }
    }
}
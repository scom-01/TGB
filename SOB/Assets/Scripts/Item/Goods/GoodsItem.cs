using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoodsItem : MonoBehaviour
{
    [Tooltip("Goods Type")]
    [SerializeField]    private GOODS_TPYE Goods;
    [SerializeField]    private int Amount;
    [SerializeField]    private int DropCount;
    [SerializeField]    private GameObject GoodsPrefab;
    [SerializeField]    private Sprite GoodsSprite;
    [SerializeField]    private AudioClip EquipSoundClip;
    [SerializeField]    private float CircleSize;
    [SerializeField]    private float DestroyTime;

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
            goodsData.Goods = Goods;
            goodsData.Amount = Amount;
            goodsData.EquipSoundClip = EquipSoundClip;
            goodsData.InvokeTime = DestroyTime;    //DestroyTime
            var vec = Vector2.right * Random.Range(-2f, 2f) + Vector2.up * Random.Range(100f, 500f);
            goodsData.RB2D.AddForce(vec);
            goodsData.isInit = true;
        }
    }
}
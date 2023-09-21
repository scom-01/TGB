using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoodsItem : MonoBehaviour
{
    [Tooltip("Goods Type")]
    [SerializeField]    private GOODS_TPYE Goods;
    /// <summary>
    /// 개당 재화량
    /// </summary>
    [SerializeField]    private int Amount;
    /// <summary>
    /// 드랍될 재화아이템 갯수
    /// </summary>
    [SerializeField]    private int DropCount;
    /// <summary>
    /// 재화 Prefaab
    /// </summary>
    [SerializeField]    private GameObject GoodsPrefab;
    /// <summary>
    /// 재화 Sprite
    /// </summary>
    [SerializeField]    private Sprite GoodsSprite;
    /// <summary>
    /// 재화 습득 사운드클립
    /// </summary>
    [SerializeField]    private AudioClip EquipSoundClip;
    /// <summary>
    /// 재화 습득 이펙트
    /// </summary>
    [SerializeField]    private GameObject EquipEffect;
    /// <summary>
    /// 재화의 Circle Radius
    /// </summary>
    [SerializeField]    private float CircleSize;
    /// <summary>
    /// 재화의 삭제 딜레이 시간
    /// </summary>
    [SerializeField]    private float DestroyTime;
    private Vector3 spawnPos;
    private GameObject Core; 
    private void Start()
    {
        Core = gameObject.GetComponentInParent<Unit>().Core.gameObject;
    }
    public void DropGoods()
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
            return;

        spawnPos = Core.transform.position;
        StartCoroutine(SpawnGoods());
    }

    IEnumerator SpawnGoods()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < DropCount; i++)
        {
            var goods = Instantiate(GoodsPrefab, GameManager.Inst.StageManager.IM.transform);
            var goodsData = goods.GetComponent<GoodsData>();
            goodsData.transform.position = spawnPos;
            goodsData.SR.sprite = GoodsSprite;
            goodsData.CircleSize = CircleSize;
            goodsData.Goods = Goods;
            goodsData.Amount = Amount;
            goodsData.EquipSoundClip = EquipSoundClip;
            goodsData.EquipEffect = EquipEffect;
            goodsData.InvokeTime = DestroyTime;    //DestroyTime
            var vec = Vector2.right * Random.Range(-2f, 2f) + Vector2.up * Random.Range(400f, 700f);
            goodsData.RB2D.AddForce(vec);
            goodsData.isInit = true;
        }
        yield return null;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TGB;
using UnityEngine;

[Serializable]
public struct GoodsData
{
    [Tooltip("Goods Type")]
    [SerializeField] public GOODS_TPYE Goods;
    [Tooltip("개당 재화량, 총 재화 = Amount * DropCount")]
    /// <summary>
    /// 개당 재화량
    /// </summary>
    [SerializeField] public int Amount;
    [Tooltip("드랍될 재화아이템 갯수")]
    /// <summary>
    /// 드랍될 재화아이템 갯수
    /// </summary>
    [SerializeField] public int DropCount;
    /// <summary>
    /// 재화 Prefaab
    /// </summary>
    [SerializeField] public GameObject GoodsPrefab;
    /// <summary>
    /// 재화 Sprite
    /// </summary>
    [SerializeField] public Sprite GoodsSprite;
    /// <summary>
    /// 재화 습득 사운드클립
    /// </summary>
    [SerializeField] public AudioPrefab EquipSoundClip;
    /// <summary>
    /// 재화 습득 이펙트
    /// </summary>
    [SerializeField] public GameObject EquipEffect;
    /// <summary>
    /// 재화의 Circle Radius
    /// </summary>
    [SerializeField] public float CircleSize;
    /// <summary>
    /// 재화의 삭제 딜레이 시간
    /// </summary>
    [SerializeField] public float DestroyTime;
    /// <summary>
    /// 재화의 삭제 딜레이 시간 랜덤 범위 (+- Random.Range)
    /// </summary>
    [SerializeField] public float DestroyTimeRange;
}

public class GoodsItem : MonoBehaviour, ISpawn
{
    public List<GoodsData> DataList = new List<GoodsData>();
    private Vector3 spawnPos;
    private GameObject Core;
    private void Start()
    {
        Core = gameObject.GetComponentInParent<Unit>()?.Core.gameObject;
    }
    public void DropGoods()
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
            return;

        if (Core != null)
        {
            spawnPos = Core.transform.position;
        }
        else
        {
            spawnPos = GameManager.Inst.StageManager.SpawnPoint.position;
        }
        foreach(var data in DataList)
        {
            StartCoroutine(SpawnGoods(data));
        }
    }

    IEnumerator SpawnGoods(GoodsData data)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < data.DropCount; i++)
        {
            var goods = Instantiate(data.GoodsPrefab, GameManager.Inst.StageManager.IM.transform);
            var goodsData = goods.GetComponent<GoodsChunk>();
            goodsData.transform.position = spawnPos + Vector3.up;
            goodsData.SR.sprite = data.GoodsSprite;
            goodsData.CircleSize = data.CircleSize;
            goodsData.Goods = data.Goods;
            goodsData.Amount = data.Amount;
            goodsData.EquipSFX = data.EquipSoundClip;
            goodsData.EquipEffect = data.EquipEffect;
            goodsData.InvokeTimeRange = data.DestroyTimeRange;
            goodsData.InvokeTime = data.DestroyTime;    //DestroyTime
            float randX = UnityEngine.Random.Range(-1f, 1f);
            float randY = UnityEngine.Random.Range(400f, 700f);
            Debug.Log($"{this.name} , randX = {randX}, randY = {randY}");
            goodsData.transform.position = goodsData.transform.position + Vector3.right * randX;
            var vec = Vector2.up * randY;
            goodsData.RB2D.AddForce(vec);
            goodsData.isInit = true;
        }
        yield return null;
    }
    
    //IEnumerator SpawnGoods()
    //{
    //    yield return new WaitForSeconds(1f);

    //    for (int i = 0; i < DropCount; i++)
    //    {
    //        var goods = Instantiate(GoodsPrefab, GameManager.Inst.StageManager.IM.transform);
    //        var goodsData = goods.GetComponent<GoodsChunk>();
    //        goodsData.transform.position = spawnPos + Vector3.up;
    //        goodsData.SR.sprite = GoodsSprite;
    //        goodsData.CircleSize = CircleSize;
    //        goodsData.Goods = Goods;
    //        goodsData.Amount = Amount;
    //        goodsData.EquipSFX = EquipSoundClip;
    //        goodsData.EquipEffect = EquipEffect;
    //        goodsData.InvokeTimeRange = DestroyTimeRange;
    //        goodsData.InvokeTime = DestroyTime;    //DestroyTime
    //        float randX = UnityEngine.Random.Range(-1f, 1f);
    //        float randY = UnityEngine.Random.Range(400f, 700f);
    //        Debug.Log($"{this.name} , randX = {randX}, randY = {randY}");
    //        goodsData.transform.position = goodsData.transform.position + Vector3.right * randX;
    //        var vec = Vector2.up * randY;
    //        goodsData.RB2D.AddForce(vec);
    //        goodsData.isInit = true;
    //    }
    //    yield return null;
    //}

    public bool Spawn()
    {
        DropGoods();
        return true;
    }
}
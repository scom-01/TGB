using System.Collections;
using System.Collections.Generic;
using TGB;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GoodsItem : MonoBehaviour, ISpawn
{
    public List<GoodsData> DataList = new List<GoodsData>();
    [HideInInspector] public Vector3 spawnPos;
    private GameObject Core;
    private void Start()
    {
        Core = gameObject.GetComponentInParent<Unit>()?.Core.gameObject;
        if (Core != null)
        {
            spawnPos = Core.transform.position;
        }
        else
        {
            spawnPos = this.transform.position;
        }
    }
    public bool Set(GoodsData data, Vector3 pos)
    {
        if (DataList == null)
            return false;
        DataList.Add(data);
        spawnPos = pos;
        return true;
    }
    public bool Add(GoodsData data)
    {
        if (DataList == null)
            return false;
        DataList.Add(data);
        return true;
    }
    public void SetPos(Vector3 _pos)
    {
        spawnPos = _pos;
        DropGoods();
    }
    public void DropGoods()
    {
        if (GameManager.Inst?.StageManager == null)
            return;

        foreach (var data in DataList)
        {
            switch (data.Goods)
            {
                case GOODS_TPYE.Gold:
                    data.SOdata = GlobalValue.Goods_Gold;
                    break;
                case GOODS_TPYE.FireGoods:
                    data.SOdata = GlobalValue.Goods_Fire;
                    break;
                case GOODS_TPYE.WaterGoods:
                    data.SOdata = GlobalValue.Goods_Water;
                    break;
                case GOODS_TPYE.EarthGoods:
                    data.SOdata = GlobalValue.Goods_Earth;
                    break;
                case GOODS_TPYE.WindGoods:
                    data.SOdata = GlobalValue.Goods_Wind;
                    break;
                case GOODS_TPYE.HammerShards:
                    break;
                case GOODS_TPYE.None:
                    break;
                default:
                    break;
            }
            StartCoroutine(SpawnGoods(data));
        }
    }

    IEnumerator SpawnGoods(GoodsData data)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < data.DropCount; i++)
        {
            var goods = Instantiate(GlobalValue.Base_GoodsChunk, GameManager.Inst.StageManager.IM.transform);
            var goodsData = goods.GetComponent<GoodsChunk>();
            goodsData.transform.position = spawnPos + Vector3.up;
            if (data.SOdata != null)
            {
                goodsData.SR.sprite = data.SOdata.GoodsSprite;
                goodsData.CircleSize = data.SOdata.CircleSize;
                goodsData.EquipSFX = data.SOdata.EquipSoundClip;
                goodsData.EquipEffect = data.SOdata.EquipEffect;
            }
            goodsData.Goods = data.Goods;
            goodsData.Amount = data.Amount;
            goodsData.InvokeTimeRange = data.DestroyTimeRange;
            goodsData.InvokeTime = data.DestroyTime;    //DestroyTime
            float randX = UnityEngine.Random.Range(-1f, 1f);
            float randY = UnityEngine.Random.Range(400f, 700f);
            Debug.Log($"{this.name} , randX = {randX}, randY = {randY}");
            goodsData.transform.position += Vector3.right * randX;
            var vec = Vector2.up * randY;
            goodsData.RB2D.AddForce(vec);
            goodsData.isInit = true;
        }
        Destroy(gameObject);
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
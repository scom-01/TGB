using Mono.Cecil;
using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataParsing;

public class GoodsData : MonoBehaviour
{
    public SpriteRenderer SR;
    public GOODS_TPYE Goods;
    public int Amount;
    public AudioClip EquipSoundClip;
    public float InvokeTime
    {
        set
        {
            if(value <= 0)
            {
                Invoke("GoodsEquip", 1.5f);
            }
            else
            {
                Invoke("GoodsEquip", value);
            }
        }
    }

    private CircleCollider2D CC2D;    
    public Rigidbody2D RB2D
    {
        get
        {
            if (rb2d == null)
                rb2d = this.GetComponent<Rigidbody2D>();
            return rb2d;
        }
    }

    private Rigidbody2D rb2d;    
    public float CircleSize
    {
        set
        {
            if(CC2D!=null)
            {
                CC2D.radius = value;
            }
        }
    }
    [HideInInspector] public bool isInit = false;

    public void Awake()
    {
        if (CC2D == null)
            CC2D = GetComponentInChildren<CircleCollider2D>();

        if (SR == null)
            SR = GetComponent<SpriteRenderer>();
    }

    public void GoodsEquip()
    {
        if (!isInit)
            return;

        if(DataManager.Inst == null)
        {
            Debug.LogWarning("DataManager is Null");
            return;
        }

        DataManager.Inst.IncreaseGoods(Goods, Amount);
        if (GameManager.Inst.StageManager != null)
        {
            if (EquipSoundClip != null)
                GameManager.Inst.StageManager.player.Core.GetCoreComponent<SoundEffect>().AudioSpawn(EquipSoundClip);
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInit)
            return;

        if (collision.tag == "Player")
        {
            GoodsEquip();
        }
    }
}
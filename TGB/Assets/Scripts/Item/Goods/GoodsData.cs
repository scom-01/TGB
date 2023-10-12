using UnityEngine;

public class GoodsData : MonoBehaviour
{
    public SpriteRenderer SR;
    public GOODS_TPYE Goods;
    public int Amount;
    public AudioClip DropSoundClip;
    public AudioClip EquipSoundClip;
    public GameObject EquipEffect;
    public float InvokeTime
    {
        set
        {
            if(value <= 0)
            {
                Invoke("GoodsEquip", Random.Range(1f,1.5f));
            }
            else
            {
                Invoke("GoodsEquip", Random.Range(value - InvokeTimeRange, value + InvokeTimeRange));
            }
        }
    }
    public float InvokeTimeRange;
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
                GameManager.Inst.StageManager.player.Core.CoreSoundEffect.AudioSpawn(EquipSoundClip);
            if (EquipEffect != null)
                GameManager.Inst.StageManager.player.Core.CoreEffectManager.StartEffects(EquipEffect, this.gameObject.transform.position);
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInit)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if (DropSoundClip != null)
                GameManager.Inst.StageManager.player.Core.CoreSoundEffect.AudioSpawn(DropSoundClip);
        }

        if (collision.tag == "Player")
        {
            GoodsEquip();
        }
    }
}
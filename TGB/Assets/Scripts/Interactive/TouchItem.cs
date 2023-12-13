using Cinemachine;
using System.Linq;
using UnityEngine;


public class TouchItem : TouchObject
{
    [TagField]
    public string[] Ignore_Tag;

    [SerializeField] private BuffItemSO Item;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    private void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("TouchObject");
        if (SpriteRenderer == null)
        {
            SpriteRenderer = this.GetComponent<SpriteRenderer>();
            if(SpriteRenderer == null)
            {
                SpriteRenderer = this.GetComponentInParent<SpriteRenderer>();
            }
        }
        if (Item != null)
            SpriteRenderer.sprite = Item.itemData.ItemSprite;
    }
    public override void Touch(GameObject obj)
    {
        base.Touch(obj);
    }

    public override void UnTouch(GameObject obj)
    {
        base.UnTouch(obj);
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (Ignore_Tag.Contains(collision.tag))
            return;

        base.OnTriggerStay2D(collision);

        if (!collision.gameObject.GetComponent<BuffSystem>())
            return;

        Collision(collision.gameObject);
    }

    private void Collision(GameObject gameObject)
    {
        if (Item != null)
        {

            Buff.BuffSystemAddBuff(gameObject.GetComponent<Unit>(), Item);

            //Vfx
            if (Item.InitEffectData.AcquiredEffectPrefab != null)
                gameObject.GetComponent<Unit>().Core.CoreEffectManager.StartEffects(Item.InitEffectData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity, Vector3.one);            

            //Sfx
            if (Item.InitEffectData.AcquiredSFX.Clip != null)
                gameObject.GetComponent<Unit>().Core.CoreSoundEffect.AudioSpawn(Item.InitEffectData.AcquiredSFX);


            Destroy(SpriteRenderer.gameObject);
        }
        else
        {
            Debug.LogWarning("itemData is null");
        }
    }
}

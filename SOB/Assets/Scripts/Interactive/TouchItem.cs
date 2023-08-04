using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchItem : TouchObject
{
    [SerializeField] private StatsItemSO Item;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    private void Awake()
    {
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
    public override void Touch()
    {
    }

    public override void UnTouch()
    {
        base.UnTouch();
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if (!collision.gameObject.GetComponent<BuffSystem>())
            return;

        Collision(collision.gameObject);
    }

    private void Collision(GameObject gameObject)
    {
        if (Item != null)
        {
            Buff buff = new Buff();
            var items = Item as BuffItemSO;
            buff.buffItemSO = items;
            gameObject.GetComponent<Unit>().Core.CoreSoundEffect.AudioSpawn(Item.effectData.AcquiredSoundEffect);
            gameObject.GetComponent<Unit>().gameObject.GetComponent<BuffSystem>().AddBuff(buff);

            if (Item.effectData.AcquiredEffectPrefab != null)
                Instantiate(Item.effectData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity, effectContainer);

            Destroy(SpriteRenderer.gameObject);
        }
        else
        {
            Debug.LogWarning("itemData is null");
        }
    }
}

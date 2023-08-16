using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class TouchItem : TouchObject
{
    [TagField]
    public string Ignore_Tag;

    [SerializeField] private StatsItemSO Item;
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
    public override void Touch()
    {
    }

    public override void UnTouch()
    {
        base.UnTouch();
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Ignore_Tag)
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
            Buff buff = new Buff();
            var items = Item as BuffItemSO;
            buff.buffItemSO = items;
            gameObject.GetComponent<Unit>().Core.CoreSoundEffect?.AudioSpawn(Item.effectData.AcquiredSoundEffect);
            gameObject.GetComponent<Unit>().gameObject.GetComponent<BuffSystem>()?.AddBuff(buff);

            //Vfx
            if (Item.effectData.AcquiredEffectPrefab != null)
                gameObject.GetComponent<Unit>().Core.CoreEffectManager.StartEffects(Item.effectData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity);
            Instantiate(Item.effectData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity, effectContainer);

            //Sfx
            if (Item.effectData.AcquiredSoundEffect != null)
                gameObject.GetComponent<Unit>().Core.CoreSoundEffect.AudioSpawn(Item.effectData.AcquiredSoundEffect);


            Destroy(SpriteRenderer.gameObject);
        }
        else
        {
            Debug.LogWarning("itemData is null");
        }
    }
}

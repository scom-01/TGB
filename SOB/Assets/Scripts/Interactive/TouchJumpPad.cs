using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchJumpPad : TouchObject
{
    [SerializeField] private float JumpVelocity;
    [SerializeField] private GameObject JumpEffectPrefab;
    private BoxCollider2D BC2D;
    private Animator animator;
    private void Awake()
    {
        if (BC2D == null)
            BC2D = this.GetComponent<BoxCollider2D>();

    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        Collision(collision.gameObject);
    }

    public override void Touch()
    {
        base.Touch();
        if (animator != null)
            animator.SetBool("Action", true);
    }

    public override void UnTouch()
    {
        base.UnTouch();
        if (animator != null)
            animator.SetBool("Action", false);
    }

    private void Collision(GameObject gameObject)
    {
        if (gameObject.GetComponent<Unit>().Core.CoreDamageReceiver.isTouch)
            return;

        gameObject.GetComponent<Unit>().Core.CoreDamageReceiver.isTouch = true;

        if (JumpEffectPrefab != null)
        {
            if (JumpEffectPrefab != null)
                Instantiate(JumpEffectPrefab, this.gameObject.transform.position, Quaternion.identity, effectContainer);
        }
        Touch();

        gameObject.GetComponent<Unit>().Core.CoreMovement.SetVelocityY(JumpVelocity);
    }
}

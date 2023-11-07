using UnityEngine;

public class TouchJumpPad : TouchObject
{
    [SerializeField] private float JumpVelocity;
    [SerializeField] private Vector2 angle;
    private BoxCollider2D BC2D;
    public Animator animator
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            return anim;
        }
    }
    private Animator anim;
    private void Awake()
    {
        if (BC2D == null)
            BC2D = this.GetComponent<BoxCollider2D>();
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        Collision(collision.GetComponent<Unit>());
    }

    public override void Touch()
    {
        base.Touch();
        if (animator != null)
            animator.Play("Action", -1, 0);
    }

    public override void UnTouch()
    {
        base.UnTouch();
        if (animator != null)
            animator.Play("UnAction", -1, 0);
    }

    private void Collision(Unit unit)
    {
        Touch();
        if (EffectObject)
            unit.Core.CoreEffectManager.StartEffects(EffectObject, unit.transform.position);
        if (SfxObject)
            unit.Core.CoreSoundEffect.AudioSpawn(SfxObject);
        //x = 0 인 위로만 올리는 점프패드일 때는 공중 움직임 제한 X
        if (angle.x == 0)
        {
            unit.Core.CoreMovement.SetVelocityY(JumpVelocity);
        }
        else
        {
            unit.Core.CoreKnockBackReceiver.TrapKnockBack(angle, JumpVelocity, false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 m_angle = angle;
        m_angle.Normalize();
        Debug.DrawRay(transform.position, new Vector3(m_angle.x, m_angle.y, 0), Color.red);
    }
}

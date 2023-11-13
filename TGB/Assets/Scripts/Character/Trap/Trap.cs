using Cinemachine;
using UnityEngine;

public class Trap : TouchObject
{
    public UnitData UnitData;
    public Vector2 knockbackAngle;
    [TagField]
    public string IgnoreTag;
    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == IgnoreTag)
        {
            return;
        }

        if (knockbackAngle.x != 0 || knockbackAngle.y != 0)
        {
            var knockback = collision.GetComponent<Unit>().Core.CoreKnockBackReceiver;
            if (knockback != null)
            {
                knockback.TrapKnockBack(knockbackAngle, knockbackAngle.magnitude);
                Debug.LogWarning(collision.name + "KnockBackReceiver" + "Trap");
            }
        }

        var damage = collision.GetComponent<Unit>().Core.CoreDamageReceiver;
        if (damage != null)
        {
            damage.TrapDamage(UnitData.statsStats, UnitData.statsStats.DefaultPower);
            Debug.LogWarning(collision.name + "DamageReceiver" + "Trap");
        }

        if (EffectObject)
            collision.GetComponent<Unit>()?.Core.CoreEffectManager.StartEffectsPos(EffectObject, collision.GetComponent<Unit>().transform.position, EffectObject.transform.localScale);
        if (SfxObject)
            collision.GetComponent<Unit>()?.Core.CoreSoundEffect.AudioSpawn(SfxObject);
    }
}

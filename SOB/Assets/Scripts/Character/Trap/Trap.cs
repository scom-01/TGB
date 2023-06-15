using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine;


public class Trap : MonoBehaviour
{
    public UnitData UnitData;
    public Vector2 knockbackAngle;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var knockback = collision.GetComponent<KnockBackReceiver>();
        if (knockback != null)
        {
            knockback.TrapKnockBack(knockbackAngle, knockbackAngle.magnitude);
            Debug.LogWarning(collision.name + "KnockBackReceiver" + "Trap");
        }

        var damage = collision.GetComponent<DamageReceiver>();
        if (damage != null) 
        {
            damage.TrapDamage(UnitData.statsStats, UnitData.statsStats.DefaultPower);
            Debug.LogWarning(collision.name + "DamageReceiver" + "Trap");
        }
    }
}

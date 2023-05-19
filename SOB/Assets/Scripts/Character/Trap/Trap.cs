using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap : MonoBehaviour
{
    public UnitData UnitData;
    public Vector2 knockbackAngle;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var damage = collision.GetComponent<DamageReceiver>();
        if (damage != null) 
        {
            damage.Damage(UnitData.statsStats, UnitData.statsStats.DefaultPower);
            Debug.LogWarning(collision.name + "DamageReceiver" + "Trap");
        }
        var knockback = collision.GetComponent<KnockBackReceiver>();
        if (knockback != null)
        {
            knockback.KnockBack(knockbackAngle, knockbackAngle.magnitude);
            Debug.LogWarning(collision.name + "KnockBackReceiver" + "Trap");
        }
    }
}

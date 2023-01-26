using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData,AttackDamage>
    {
        private ActionHitBox hitBox;
        private void HandleDetectedCollider2D(Collider2D[] coll)
        {
            foreach(var item in coll)
            {
                print($"Detected Item {item.name}");
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    //damageable.Damage(currentActionData.);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<ActionHitBox>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}

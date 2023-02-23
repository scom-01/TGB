using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class Knockback : WeaponComponent<KnockbackData, ActionKnockback>
    {

        private ActionHitBox hitBox;
        private void HandleDetectedCollider2D(Collider2D[] coll)
        {
            foreach (var detecte in coll)
            {
                if (detecte.gameObject.tag == "Player")
                    continue;

                if (detecte.TryGetComponent(out IKnockBackable knockbackables))
                {
                    knockbackables.KnockBack(currentActionData.KnockbackAngle, currentActionData.KnockbackAngle.magnitude, core.GetCoreComponent<Movement>().FancingDirection);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDestory()
        {
            base.OnDestory();

            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}

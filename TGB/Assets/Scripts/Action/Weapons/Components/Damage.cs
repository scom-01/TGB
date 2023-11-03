using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace TGB.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData, ActionDamage>
    {
        private ActionHitBox hitBox;

        protected override void HandleEnter()
        {
            base.HandleEnter();
        }
        /// <summary>
        /// Hit Collider가 감지된 오브젝트에 Damage
        /// </summary>
        /// <param name="colls"></param>
        private void HandleDetectedCollider2D(Collider2D[] colls)
        {
            if (currentActionData != null)
            {
                CheckAttackAction(currentActionData, colls);
            }
        }

        private void CheckAttackAction(ActionDamage actionDamage, Collider2D[] colls)
        {
            if (actionDamage == null)
                return;

            var currDamage = actionDamage.HitDamage;
            if (currDamage == null)
                return;

            if (currDamage.Length <= 0)
                return;

            if (currDamage.Length <= hitBox.currentHitBoxIndex)
                return;

            foreach (var coll in colls)
            {
                if (coll.gameObject.tag == this.gameObject.tag)
                    continue;

                if (coll.TryGetComponent(out IDamageable damageable))
                {
                    if (currDamage[hitBox.currentHitBoxIndex].isFixed)
                    {
                        damageable.FixedDamage((int)currDamage[hitBox.currentHitBoxIndex].AdditionalDamage, true, currDamage[hitBox.currentHitBoxIndex].RepeatAmount);
                    }
                    else
                    {
                        damageable.TypeCalDamage(core.Unit, coll.GetComponentInParent<Unit>(), CoreUnitStats.DefaultPower + currDamage[hitBox.currentHitBoxIndex].AdditionalDamage, currDamage[hitBox.currentHitBoxIndex].RepeatAmount);
                    }
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}

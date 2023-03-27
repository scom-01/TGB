using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class Knockback : WeaponComponent<KnockbackData, ActionKnockback>
    {
        private int currentActionKnockbackIndex;
        private ActionHitBox hitBox;
        private void HandleDetectedCollider2D(Collider2D[] coll)
        {
            if (currentGroundedActionData != null && currentAirActionData != null)
            {
                if (weapon.InAir)
                {
                    CheckKnockbackAction(currentAirActionData, coll);
                }
                else
                {
                    CheckKnockbackAction(currentGroundedActionData, coll);
                }
            }
            else if (currentGroundedActionData == null)
            {
                CheckKnockbackAction(currentAirActionData, coll);
            }
            else if (currentAirActionData == null)
            {
                CheckKnockbackAction(currentGroundedActionData, coll);
            }
        }
        private void CheckKnockbackAction(ActionKnockback actionKnockback, Collider2D[] coll)
        {
            if (actionKnockback == null)
                return;

            var currKnockback = actionKnockback.Knockback;
            if (currKnockback.Length <= 0)
                return;

            for (int i = 0; i < currKnockback.Length; i++)
            {
                if (currKnockback[i].Command == weapon.Command)
                {
                    currentActionKnockbackIndex = i;
                    break;
                }
                currentActionKnockbackIndex = -1;
            }
            if (currentActionKnockbackIndex == -1)
            {
                weapon.EventHandler.AnimationFinishedTrigger();
                return;
            }

            foreach (var detecte in coll)
            {
                if (detecte.gameObject.tag == this.gameObject.tag)
                    continue;

                if (detecte.TryGetComponent(out IKnockBackable knockbackables))
                {
                    knockbackables.KnockBack(currKnockback[currentActionKnockbackIndex].KnockbackAngle, currKnockback[currentActionKnockbackIndex].KnockbackAngle.magnitude, CoreMovement.FancingDirection);
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

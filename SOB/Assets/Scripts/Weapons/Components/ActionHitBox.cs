using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private void HandleAttackAction()
        {
            Debug.Log("Handle Attack Action");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            eventHandler.OnAttackAction -= HandleAttackAction;
        }
    }
}
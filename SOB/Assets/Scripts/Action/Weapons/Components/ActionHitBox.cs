using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Progress;

namespace SOB.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private Vector2 offset;
        private Collider2D[] detected;

        private void HandleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentActionData.HitBox.center.x * CoreMovement.FancingDirection),
                transform.position.y + (currentActionData.HitBox.center.y)
                );

            detected = Physics2D.OverlapBoxAll(offset, currentActionData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
            {
                Debug.Log("detected.Length == 0"); 
                return;
            }

            OnDetectedCollider2D?.Invoke(detected);
        }

        protected override void Start()
        {
            base.Start();
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        //Hierarchy에서 선택 시 기즈모 표시
        private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach(var item in data.ActionData)
            {
                if (!item.Debug)
                    continue;                
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + new Vector3(item.HitBox.center.x * CoreMovement.FancingDirection, item.HitBox.center.y, 0), item.HitBox.size);
            }
        }
    }
}
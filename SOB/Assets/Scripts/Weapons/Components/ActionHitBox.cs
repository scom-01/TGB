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
        private event Action<Collider2D[]> OnDetectedCollider2D;
        private CoreComp<CoreSystem.Movement> movement;

        private Vector2 offset;
        private Collider2D[] detected;
        private void HandleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.center.x * movement.Comp.FancingDirection),
                transform.position.y + (currentAttackData.HitBox.center.y)
                );
            Debug.Log(offset);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
            {
                Debug.Log("detected.Length == 0");
                return;
            }

            OnDetectedCollider2D?.Invoke(detected);

            foreach(var item in detected)
            {
                Debug.Log(item.name);
            }
        }

        protected override void Start()
        {
            base.Start();

            movement = new CoreComp<CoreSystem.Movement>(core);
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

        //Hierarchy에서 선택 시 기즈모 표시
        private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach(var item in data.AttackData)
            {
                if (!item.Debug)
                    continue;                
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + new Vector3(item.HitBox.center.x * movement.Comp.FancingDirection, item.HitBox.center.y, 0), item.HitBox.size);
            }
        }
    }
}
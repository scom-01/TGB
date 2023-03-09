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


        private int currentHitBoxIndex;
        private int currentHitEffectIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentHitBoxIndex = 0;
            currentHitEffectIndex = 0;
        }

        private void HandleAttackAction()
        {
            var currHitBox = currentActionData.HitBox;
            if(currentHitBoxIndex >= currHitBox.Length)
            {
                Debug.Log($"{weapon.name} HitBox length mismatch");
                return;
            }

            offset.Set(
                transform.position.x + (currentActionData.HitBox[currentHitBoxIndex].center.x * CoreMovement.FancingDirection),
                transform.position.y + (currentActionData.HitBox[currentHitBoxIndex].center.y)
                );

            detected = Physics2D.OverlapBoxAll(offset, currentActionData.HitBox[currentHitBoxIndex].size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
            {
                Debug.Log("detected.Length == 0"); 
                return;
            }

            var currentHit = currentActionData.HitEffect;
            if (currentHitEffectIndex >= currentHit.Length)
            {
                Debug.Log($"{weapon.name} HitEffect length mismatch");
                return;
            }

            foreach (Collider2D c in detected)
            {
                if (c.gameObject.tag == this.gameObject.tag)
                    continue;

                if (c.TryGetComponent(out IDamageable damageable))
                {
                    damageable.HitAction(currentActionData.HitEffect[currentHitEffectIndex], 0.5f);
                }
            }

            OnDetectedCollider2D?.Invoke(detected);
            currentHitBoxIndex++;
            currentHitEffectIndex++;
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
                Gizmos.DrawWireCube(transform.position + new Vector3(item.HitBox[currentHitBoxIndex].center.x * CoreMovement.FancingDirection, item.HitBox[currentHitBoxIndex].center.y, 0), item.HitBox[currentHitBoxIndex].size);
            }
        }
    }
}
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
            var currHitBox = currentActionData.ActionHit;
            if(currentHitBoxIndex >= currHitBox.Length)
            {
                Debug.Log($"{weapon.name} HitBox length mismatch");
                return;
            }

            offset.Set(
                transform.position.x + (currentActionData.ActionHit[currentHitBoxIndex].ActionRect.center.x * CoreMovement.FancingDirection),
                transform.position.y + (currentActionData.ActionHit[currentHitBoxIndex].ActionRect.center.y)
                );

            detected = Physics2D.OverlapBoxAll(offset, currentActionData.ActionHit[currentHitBoxIndex].ActionRect.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
            {
                Debug.Log("detected.Length == 0"); 
                return;
            }

            var currentHit = currentActionData.ActionHit;
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
                    damageable.HitAction(currentActionData.ActionHit[currentHitEffectIndex].EffectPrefab, 0.5f);
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

            foreach (var item in data.ActionData)
            {
                foreach(var action in item.ActionHit)
                {
                    if (!action.Debug)
                        continue;
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(transform.position + new Vector3(action.ActionRect.center.x * CoreMovement.FancingDirection, action.ActionRect.center.y, 0), action.ActionRect.size);                    
                }                
            }
        }
    }
}
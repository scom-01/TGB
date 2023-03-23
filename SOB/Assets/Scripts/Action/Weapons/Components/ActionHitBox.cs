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
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentHitBoxIndex = 0;
        }

        private void HandleAttackAction()
        {
            var currHitBox = currentActionData.ActionHit;
            if (weapon.BaseGameObject.GetComponent<Animator>().GetBool("inAir"))
            {
                currHitBox = currentActionData.IsAirActionHit;
            }

            for (int i = 0; i < currHitBox.Length; i++)
            {
                if (currHitBox[i].Command == weapon.Command)
                {
                    currentHitBoxIndex = i;
                    break;
                }
            }
            if (currentHitBoxIndex >= currHitBox.Length)
            {
                Debug.Log($"{weapon.name} HitBox length mismatch");
                return;
            }

            offset.Set(
                transform.position.x + (currHitBox[currentHitBoxIndex].ActionRect.center.x * CoreMovement.FancingDirection),
                transform.position.y + (currHitBox[currentHitBoxIndex].ActionRect.center.y)
                );

            if(weapon.Command == currHitBox[currentHitBoxIndex].Command)
            {
                detected = Physics2D.OverlapBoxAll(offset, currHitBox[currentHitBoxIndex].ActionRect.size, 0f, data.DetectableLayers);
            }

            if (detected.Length == 0)
            {
                Debug.Log("detected.Length == 0");
                return;
            }

            #region HitAction Effect Spawn
            foreach (Collider2D c in detected)
            {
                if (c.gameObject.tag == this.gameObject.tag)
                    continue;

                if (c.TryGetComponent(out IDamageable damageable))
                {
                    if (currHitBox[currentHitBoxIndex].EffectPrefab == null)
                    {
                        continue;
                    }
                    for (int i = 0; i < currHitBox[currentHitBoxIndex].EffectPrefab.Length; i++)
                    {
                        //if (currentActionData.ActionHit[currentHitEffectIndex].EffectPrefab[i] == null)
                        //{
                        //    continue;
                        //}
                        damageable.HitAction(currHitBox[currentHitBoxIndex].EffectPrefab[i], 0.5f);
                    }
                }
            }
            #endregion

            OnDetectedCollider2D?.Invoke(detected);
            currentHitBoxIndex++;

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
                if (item.ActionHit == null || item.IsAirActionHit == null)
                    continue;

                foreach (var action in item.ActionHit)
                {
                    if (!action.Debug)
                        continue;
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(transform.position + new Vector3(action.ActionRect.center.x * CoreMovement.FancingDirection, action.ActionRect.center.y, 0), action.ActionRect.size);
                }

                foreach (var action in item.IsAirActionHit)
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
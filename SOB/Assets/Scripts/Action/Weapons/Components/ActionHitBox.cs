using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Progress;
using UnityEditor.Timeline.Actions;

namespace SOB.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private Vector2 offset;
        private Collider2D[] detected;

        public int currentHitBoxIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentHitBoxIndex = 0;
        }

        private void HandleAttackAction()
        {
            if (currentActionData != null)
            {
                CheckAttackAction(currentActionData);
            }

            //if (currentGroundedActionData != null && currentAirActionData != null)
            //{
            //    if (weapon.InAir)
            //    {
            //        CheckAttackAction(currentAirActionData);
            //    }
            //    else
            //    {
            //        CheckAttackAction(currentGroundedActionData);
            //    }
            //}
            //else if (currentGroundedActionData == null)
            //{
            //    CheckAttackAction(currentAirActionData);
            //}
            //else if (currentAirActionData == null)
            //{
            //    CheckAttackAction(currentGroundedActionData);
            //}

            OnDetectedCollider2D?.Invoke(detected);
            currentHitBoxIndex++;

        }

        private void CheckAttackAction(AttackActionHitBox actionData)
        {
            if (actionData == null)
                return;

            var currHitBox = actionData.ActionHit;
            if (currHitBox.Length <= 0)
                return;

            offset.Set(
                    transform.position.x + (currHitBox[currentHitBoxIndex].ActionRect.center.x * CoreMovement.FancingDirection),
                    transform.position.y + (currHitBox[currentHitBoxIndex].ActionRect.center.y)
                    );
                        
            detected = Physics2D.OverlapBoxAll(offset, currHitBox[currentHitBoxIndex].ActionRect.size, 0f, data.DetectableLayers);

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
                        damageable.HitAction(currHitBox[currentHitBoxIndex].EffectPrefab[i], 0.5f);
                    }
                }
            }
            #endregion
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
                if (item.ActionHit == null)
                    continue;

                foreach (var action in item.ActionHit)
                {
                    if (!action.Debug)
                        continue;
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(transform.position + new Vector3(action.ActionRect.center.x * CoreMovement.FancingDirection, action.ActionRect.center.y, 0), action.ActionRect.size);
                }

                foreach (var action in item.ActionHit)
                {
                    if (!action.Debug)
                        continue;
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(transform.position + new Vector3(action.ActionRect.center.x * CoreMovement.FancingDirection, action.ActionRect.center.y, 0), action.ActionRect.size);
                }
            }
            //foreach (var item in data.InAirActionData)
            //{
            //    if (item.ActionHit == null)
            //        continue;

            //    foreach (var action in item.ActionHit)
            //    {
            //        if (!action.Debug)
            //            continue;
            //        Gizmos.color = Color.white;
            //        Gizmos.DrawWireCube(transform.position + new Vector3(action.ActionRect.center.x * CoreMovement.FancingDirection, action.ActionRect.center.y, 0), action.ActionRect.size);
            //    }

            //    foreach (var action in item.ActionHit)
            //    {
            //        if (!action.Debug)
            //            continue;
            //        Gizmos.color = Color.white;
            //        Gizmos.DrawWireCube(transform.position + new Vector3(action.ActionRect.center.x * CoreMovement.FancingDirection, action.ActionRect.center.y, 0), action.ActionRect.size);
            //    }
            //}
        }
    }
}
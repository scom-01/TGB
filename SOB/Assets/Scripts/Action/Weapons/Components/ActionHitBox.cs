using SOB.CoreSystem;
using System;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private Vector2 offset;
        private Collider2D[] detected;

        public int currentHitBoxIndex = 0;
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
            foreach (Collider2D coll in detected)
            {
                if (coll.gameObject.tag == this.gameObject.tag)
                    continue;

                if (coll.gameObject.tag == "Trap")
                    continue;

                //객체 사망 시 무시
                if(coll.gameObject.GetComponentInParent<Unit>().Core.GetCoreComponent<Death>().isDead)
                {
                    continue;
                }

                if (coll.gameObject.GetComponentInParent<Enemy>() != null)
                {
                    coll.gameObject.GetComponentInParent<Enemy>().SetTarget(core.Unit);
                }

                //Hit시 효과
                if (coll.TryGetComponent(out IDamageable damageable))
                {
                    core.Unit.Inventory.ItemEffectExecute(core.Unit, coll.GetComponentInParent<Unit>());
                    //EffectPrefab
                    if (currHitBox[currentHitBoxIndex].EffectPrefab != null)
                    {
                        for (int i = 0; i < currHitBox[currentHitBoxIndex].EffectPrefab.Length; i++)
                        {
                            damageable.HitAction(currHitBox[currentHitBoxIndex].EffectPrefab[i].Object, currHitBox[currentHitBoxIndex].EffectPrefab[i].isRandomRange, CoreMovement.FancingDirection);
                        }
                    }

                    //AudioClip
                    if (currHitBox[currentHitBoxIndex].audioClip != null)
                    {
                        for (int i = 0; i < currHitBox[currentHitBoxIndex].audioClip.Length; i++)
                        {
                            CoreSoundEffect.AudioSpawn(currHitBox[currentHitBoxIndex].audioClip[i]);
                        }
                    }

                    //ShakeCam
                    if (currHitBox[currentHitBoxIndex].camDatas != null)
                    {
                        for (int i = 0; i < currHitBox[currentHitBoxIndex].camDatas.Length; i++)
                        {
                            Camera.main.GetComponent<CameraShake>().Shake(
                                currHitBox[currentHitBoxIndex].camDatas[i].RepeatRate,
                                currHitBox[currentHitBoxIndex].camDatas[i].Range,
                                currHitBox[currentHitBoxIndex].camDatas[i].Duration
                                );
                        }
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


        private void OnDrawGizmos()
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
            }
        }
    }
}
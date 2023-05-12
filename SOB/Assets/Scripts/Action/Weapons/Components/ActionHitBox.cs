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
                
                //객체 사망 시 무시
                if(c.gameObject.GetComponentInParent<Unit>().Core.GetCoreComponent<Death>().isDead)
                {
                    continue;
                }
                //Hit시 효과
                if (c.TryGetComponent(out IDamageable damageable))
                {
                    core.Unit.Inventory.ItemEffectExcute();
                    //EffectPrefab
                    if (currHitBox[currentHitBoxIndex].EffectPrefab != null)
                    {
                        for (int i = 0; i < currHitBox[currentHitBoxIndex].EffectPrefab.Length; i++)
                        {
                            //if (currHitBox[currentHitBoxIndex].EffectPrefab[i].isGround)
                            //{
                            //    CoreParticleManager.StartParticles(currHitBox[currentHitBoxIndex].EffectPrefab[i].Object, CoreCollisionSenses.GroundCheck.position);
                            //}
                            //else
                            //{
                            //    if (currHitBox[currentHitBoxIndex].EffectPrefab[i].isRandomPosRot)
                            //    {
                            //        CoreParticleManager.StartParticlesWithRandomPosRot(
                            //                currHitBox[currentHitBoxIndex].EffectPrefab[i].Object,
                            //                currHitBox[currentHitBoxIndex].EffectPrefab[i].isRandomRange);
                            //    }
                            //    else
                            //    {
                            //        CoreParticleManager.StartParticles(currHitBox[currentHitBoxIndex].EffectPrefab[i].Object);
                            //    }
                            //}
                            damageable.HitAction(currHitBox[currentHitBoxIndex].EffectPrefab[i].Object, currHitBox[currentHitBoxIndex].EffectPrefab[i].isRandomRange);
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
                                currHitBox[currentHitBoxIndex].camDatas[i].ShakgeCamRepeatRate,
                                currHitBox[currentHitBoxIndex].camDatas[i].ShakeCamRange,
                                currHitBox[currentHitBoxIndex].camDatas[i].ShakeCamDuration
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
        }
    }
}
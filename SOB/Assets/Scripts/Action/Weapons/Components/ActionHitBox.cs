using SOB.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace SOB.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private Vector2 offset;
        private Collider2D[] detected;
        private BoxCollider2D Box2D
        {
            get
            {
                if (box2D == null)
                {
                    box2D = this.GetComponent<BoxCollider2D>();
                    if (box2D == null)
                    {
                        box2D = this.AddComponent<BoxCollider2D>();
                    }
                    box2D.isTrigger = true;
                }
                return box2D;
            }
        }

        private BoxCollider2D box2D;

        private bool isTriggerOn;
        private List<Collider2D> actionHitObjects = new List<Collider2D>();
        private HitAction[] hitActions = null;

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
                if (coll.gameObject.GetComponentInParent<Unit>().Core.GetCoreComponent<Death>().isDead)
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
                    for (int j = 0; j < currHitBox[currentHitBoxIndex].RepeatAction; j++)
                    {
                        core.Unit.Inventory.ItemEffectExecute(core.Unit, coll.GetComponentInParent<Unit>());

                        //EffectPrefab
                        #region EffectPrefab
                        if (currHitBox[currentHitBoxIndex].EffectPrefab != null)
                        {
                            for (int i = 0; i < currHitBox[currentHitBoxIndex].EffectPrefab.Length; i++)
                            {
                                damageable.HitEffect(currHitBox[currentHitBoxIndex].EffectPrefab[i].Object, currHitBox[currentHitBoxIndex].EffectPrefab[i].isRandomRange, CoreMovement.FancingDirection);
                            }
                        }
                        #endregion

                        //AudioClip
                        #region AudioClip
                        if (currHitBox[currentHitBoxIndex].audioClip != null)
                        {
                            for (int i = 0; i < currHitBox[currentHitBoxIndex].audioClip.Length; i++)
                            {
                                CoreSoundEffect.AudioSpawn(currHitBox[currentHitBoxIndex].audioClip[i]);
                            }
                        }
                        #endregion

                    }//ShakeCam
                    #region ShakeCam
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
                    #endregion
                }

                //Damage
                if (coll.TryGetComponent(out IDamageable _damageable))
                {
                    for (int j = 0; j < currHitBox[currentHitBoxIndex].RepeatAction; j++)
                    {
                        #region Damage
                        //플레이어는 EnemyType이 없기때문에 생략
                        if (coll.gameObject.tag == "Player")
                        {
                            _damageable.Damage
                            (
                                CoreUnitStats.StatsData,
                                coll.GetComponentInParent<Unit>().UnitData.statsStats,
                                CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage,
                                currHitBox[currentHitBoxIndex].RepeatAction
                            );
                            continue;
                        }

                        switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                        {
                            case ENEMY_Size.Small:
                                _damageable.Damage
                            (
                                CoreUnitStats.StatsData,
                                coll.GetComponentInParent<Unit>().UnitData.statsStats,
                                (CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                                currHitBox[currentHitBoxIndex].RepeatAction
                            );
                                Debug.Log("Enemy Type Small, Normal Dam = " +
                                    CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                                break;
                            case ENEMY_Size.Medium:
                                _damageable.Damage
                            (
                                CoreUnitStats.StatsData,
                                coll.GetComponentInParent<Unit>().UnitData.statsStats,
                                (CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage),
                                currHitBox[currentHitBoxIndex].RepeatAction
                                );
                                break;
                            case ENEMY_Size.Big:
                                _damageable.Damage
                            (
                                CoreUnitStats.StatsData,
                                coll.GetComponentInParent<Unit>().UnitData.statsStats,
                                (CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                                currHitBox[currentHitBoxIndex].RepeatAction
                            );

                                Debug.Log("Enemy Type Big, Normal Dam = " +
                                    CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.StatsData.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                                    );
                                break;
                        }
                    }
                    #endregion
                }

                //KnockBack
                #region KnockBack
                if (coll.TryGetComponent(out IKnockBackable knockbackables))
                {
                    knockbackables.KnockBack(currHitBox[currentHitBoxIndex].KnockbackAngle, currHitBox[currentHitBoxIndex].KnockbackAngle.magnitude, CoreMovement.FancingDirection);
                }
                #endregion

            }
            #endregion
        }

        private void HandleActionRectOn()
        {
            if (currentActionData != null)
            {
                CheckActionRect(currentActionData);
            }
        }

        private void HandleActionRectOff()
        {
            foreach (var obj in actionHitObjects)
            {
                Debug.Log($"공격 받았던 오브젝트 {obj.name}");
            }
            actionHitObjects.Clear();
            hitActions = null;
            isTriggerOn = false;

            currentHitBoxIndex++;
        }

        private void CheckActionRect(AttackActionHitBox actionData)
        {
            if (actionData == null)
                return;

            var currHitBox = actionData.ActionHit;
            if (currHitBox.Length <= 0)
                return;

            hitActions = currHitBox;
            offset.Set(
                    (currHitBox[currentHitBoxIndex].ActionRect.center.x),
                    (currHitBox[currentHitBoxIndex].ActionRect.center.y)
                    );
            Box2D.offset = offset;
            Box2D.size = currHitBox[currentHitBoxIndex].ActionRect.size;

            isTriggerOn = true;
        }

        protected override void Start()
        {
            base.Start();
            eventHandler.OnAttackAction += HandleAttackAction;
            eventHandler.OnActionRectOn += HandleActionRectOn;
            eventHandler.OnActionRectOff += HandleActionRectOff;
        }

        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnAttackAction -= HandleAttackAction;
            eventHandler.OnActionRectOn -= HandleActionRectOn;
            eventHandler.OnActionRectOff -= HandleActionRectOff;
        }

        private void OnTriggerStay2D(Collider2D coll)
        {
            if (!isTriggerOn)
                return;

            Debug.Log($"{coll.name} Trigger Enter");
            //data.DetectableLayers가 아니면 return
            if ((data.DetectableLayers.value & (1 << coll.gameObject.layer)) <= 0)
                return;

            if (coll.gameObject.tag == this.gameObject.tag)
                return;

            if (coll.gameObject.tag == "Trap")
                return;

            if (hitActions == null)
                return;

            //중복 무시
            if (actionHitObjects.Contains(coll))
            {
                Debug.Log($"{coll.name}은 이미 공격받았습니다.");
                return;
            }
            //객체 사망 시 무시
            if (coll.gameObject.GetComponentInParent<Unit>().Core.GetCoreComponent<Death>().isDead)
            {
                return;
            }

            if (coll.gameObject.GetComponentInParent<Enemy>() != null)
            {
                coll.gameObject.GetComponentInParent<Enemy>().SetTarget(core.Unit);
            }

            //중복히트를 제거하기 위한 처리
            actionHitObjects.Add(coll);

            //Hit시 효과
            if (coll.TryGetComponent(out IDamageable damageable))
            {
                for (int j = 0; j < hitActions[currentHitBoxIndex].RepeatAction; j++)
                {
                    core.Unit.Inventory.ItemEffectExecute(core.Unit, coll.GetComponentInParent<Unit>());
                    //EffectPrefab
                    if (hitActions[currentHitBoxIndex].EffectPrefab != null)
                    {
                        for (int i = 0; i < hitActions[currentHitBoxIndex].EffectPrefab.Length; i++)
                        {
                            damageable.HitEffect(hitActions[currentHitBoxIndex].EffectPrefab[i].Object, hitActions[currentHitBoxIndex].EffectPrefab[i].isRandomRange, CoreMovement.FancingDirection);
                        }
                    }

                    //AudioClip
                    if (hitActions[currentHitBoxIndex].audioClip != null)
                    {
                        for (int i = 0; i < hitActions[currentHitBoxIndex].audioClip.Length; i++)
                        {
                            CoreSoundEffect.AudioSpawn(hitActions[currentHitBoxIndex].audioClip[i]);
                        }
                    }
                }
                //ShakeCam
                if (hitActions[currentHitBoxIndex].camDatas != null)
                {
                    for (int i = 0; i < hitActions[currentHitBoxIndex].camDatas.Length; i++)
                    {
                        Camera.main.GetComponent<CameraShake>().Shake(
                            hitActions[currentHitBoxIndex].camDatas[i].RepeatRate,
                            hitActions[currentHitBoxIndex].camDatas[i].Range,
                            hitActions[currentHitBoxIndex].camDatas[i].Duration
                            );
                    }
                }
            }

            //Damage
            if (coll.TryGetComponent(out IDamageable _damageable))
            {
                for (int j = 0; j < hitActions[currentHitBoxIndex].RepeatAction; j++)
                {
                    #region Damage
                    //플레이어는 EnemyType이 없기때문에 생략
                    if (coll.gameObject.tag == "Player")
                    {
                        _damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage,
                            hitActions[currentHitBoxIndex].RepeatAction
                        );
                        continue;
                    }

                    switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                    {
                        case ENEMY_Size.Small:
                            _damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                            hitActions[currentHitBoxIndex].RepeatAction
                        );
                            Debug.Log("Enemy Type Small, Normal Dam = " +
                                CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex]
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                            break;
                        case ENEMY_Size.Medium:
                            _damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage),
                            hitActions[currentHitBoxIndex].RepeatAction
                            );
                            break;
                        case ENEMY_Size.Big:
                            _damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                            hitActions[currentHitBoxIndex].RepeatAction
                        );

                            Debug.Log("Enemy Type Big, Normal Dam = " +
                                CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex]
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (CoreUnitStats.StatsData.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                                );
                            break;
                    }
                }
                #endregion
            }

            //KnockBack
            #region KnockBack
            if (coll.TryGetComponent(out IKnockBackable knockbackables))
            {
                knockbackables.KnockBack(hitActions[currentHitBoxIndex].KnockbackAngle, hitActions[currentHitBoxIndex].KnockbackAngle.magnitude, CoreMovement.FancingDirection);
            }
            #endregion
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
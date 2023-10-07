using TGB.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace TGB.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private Vector2 offset;
        private Collider2D[] detected;
        private RaycastHit2D[] RayCastdetected;

        private bool isTriggerOn;
        private List<Collider2D> actionHitObjects = new List<Collider2D>();
        private HitAction[] hitActions = null;

        private Vector2 PosOffset;

        public int currentHitBoxIndex = 0;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentHitBoxIndex = 0;
        }
        
        private void HandleAction()
        {
            if (currentActionData == null)
                return;

            //Action 시 효과
            core.Unit.Inventory.ItemActionExecute(core.Unit);
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
                if (coll.gameObject.GetComponentInParent<Unit>().Core.CoreDeath.isDead)
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
                        core.Unit.Inventory.ItemOnHitExecute(core.Unit, coll.GetComponentInParent<Unit>());

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
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage,
                                currHitBox[currentHitBoxIndex].RepeatAction
                            );
                            continue;
                        }

                        switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                        {
                            case ENEMY_Size.Small:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                                currHitBox[currentHitBoxIndex].RepeatAction
                            );
                                Debug.Log("Enemy Type Small, Normal Dam = " +
                                    CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                                break;
                            case ENEMY_Size.Medium:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage),
                                currHitBox[currentHitBoxIndex].RepeatAction
                                );
                                break;
                            case ENEMY_Size.Big:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                                currHitBox[currentHitBoxIndex].RepeatAction
                            );

                                Debug.Log("Enemy Type Big, Normal Dam = " +
                                    CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.DefaultPower + currHitBox[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
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
            PosOffset = core.Unit.transform.position;
            Debug.Log($"PosOffset = {PosOffset}");
            if (currentActionData != null)
            {
                CheckActionRect(currentActionData);
            }
        }

        private void HandleActionRectOff()
        {
            Vector2 oldPos = core.Unit.transform.position;
            Debug.Log($"CurrPos = {oldPos}");
            var temp = (PosOffset - oldPos);
            Debug.Log($"Temp = {temp}");
            offset.Set(
                    transform.position.x + (hitActions[currentHitBoxIndex].ActionRect.center.x * CoreMovement.FancingDirection),
                    transform.position.y + (hitActions[currentHitBoxIndex].ActionRect.center.y)
                    );

            RayCastdetected = Physics2D.BoxCastAll(offset, hitActions[currentHitBoxIndex].ActionRect.size, 0f, temp, temp.magnitude, data.DetectableLayers);

            #region HitAction Effect Spawn
            for (int k = 0; k < RayCastdetected.Length; k++)
            {
                var coll = RayCastdetected[k].collider;

                if (coll.gameObject.tag == this.gameObject.tag)
                    continue;

                if (coll.gameObject.tag == "Trap")
                    continue;

                //객체 사망 시 무시
                if (coll.gameObject.GetComponentInParent<Unit>().Core.CoreDeath.isDead)
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
                    for (int j = 0; j < hitActions[currentHitBoxIndex].RepeatAction; j++)
                    {
                        core.Unit.Inventory.ItemOnHitExecute(core.Unit, coll.GetComponentInParent<Unit>());

                        //EffectPrefab
                        #region EffectPrefab
                        if (hitActions[currentHitBoxIndex].EffectPrefab != null)
                        {
                            for (int i = 0; i < hitActions[currentHitBoxIndex].EffectPrefab.Length; i++)
                            {
                                damageable.HitEffect(hitActions[currentHitBoxIndex].EffectPrefab[i].Object, hitActions[currentHitBoxIndex].EffectPrefab[i].isRandomRange, CoreMovement.FancingDirection);
                            }
                        }
                        #endregion

                        //AudioClip
                        #region AudioClip
                        if (hitActions[currentHitBoxIndex].audioClip != null)
                        {
                            for (int i = 0; i < hitActions[currentHitBoxIndex].audioClip.Length; i++)
                            {
                                CoreSoundEffect.AudioSpawn(hitActions[currentHitBoxIndex].audioClip[i]);
                            }
                        }
                        #endregion

                    }//ShakeCam
                    #region ShakeCam
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
                    #endregion
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
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage,
                                hitActions[currentHitBoxIndex].RepeatAction
                            );
                            continue;
                        }

                        switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                        {
                            case ENEMY_Size.Small:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                                hitActions[currentHitBoxIndex].RepeatAction
                            );
                                Debug.Log("Enemy Type Small, Normal Dam = " +
                                    CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                                break;
                            case ENEMY_Size.Medium:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage),
                                hitActions[currentHitBoxIndex].RepeatAction
                                );
                                break;
                            case ENEMY_Size.Big:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                                hitActions[currentHitBoxIndex].RepeatAction
                            );

                                Debug.Log("Enemy Type Big, Normal Dam = " +
                                    CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
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
            #endregion

            foreach (var obj in actionHitObjects)
            {
                Debug.Log($"공격 받았던 오브젝트 {obj.name}");
            }
            actionHitObjects.Clear();
            hitActions = null;
            isTriggerOn = false;

            currentHitBoxIndex++;
        }

        private void HandleRushActionRectOn()
        {
            PosOffset = core.Unit.transform.position;
            Debug.Log($"PosOffset = {PosOffset}");
            if (currentActionData != null)
            {
                CheckRushActionRect(currentActionData);
            }
        }

        //HandleRushActionRectOff()전에 OnTeleportToTarget을 해야한다
        private void HandleRushActionRectOff()
        {
            Vector2 oldPos = core.Unit.transform.position;
            Debug.Log($"CurrPos = {oldPos}");
            var temp = (PosOffset - oldPos);
            Debug.Log($"Temp = {temp}");
            offset.Set(
                    transform.position.x + (hitActions[currentHitBoxIndex].ActionRect.center.x * CoreMovement.FancingDirection),
                    transform.position.y + (hitActions[currentHitBoxIndex].ActionRect.center.y)
                    );

            float angle = Quaternion.Angle(Quaternion.Euler(PosOffset),Quaternion.Euler(oldPos));
            RayCastdetected = Physics2D.BoxCastAll(offset, hitActions[currentHitBoxIndex].ActionRect.size, angle, temp, temp.magnitude, data.DetectableLayers);

            #region HitAction Effect Spawn
            for (int k = 0; k < RayCastdetected.Length; k++)
            {
                var coll = RayCastdetected[k].collider;

                if (coll.gameObject.tag == this.gameObject.tag)
                    continue;

                if (coll.gameObject.tag == "Trap")
                    continue;

                //객체 사망 시 무시
                if (coll.gameObject.GetComponentInParent<Unit>().Core.CoreDeath.isDead)
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
                    for (int j = 0; j < hitActions[currentHitBoxIndex].RepeatAction; j++)
                    {
                        core.Unit.Inventory.ItemOnHitExecute(core.Unit, coll.GetComponentInParent<Unit>());

                        //EffectPrefab
                        #region EffectPrefab
                        if (hitActions[currentHitBoxIndex].EffectPrefab != null)
                        {
                            for (int i = 0; i < hitActions[currentHitBoxIndex].EffectPrefab.Length; i++)
                            {
                                damageable.HitEffect(hitActions[currentHitBoxIndex].EffectPrefab[i].Object, hitActions[currentHitBoxIndex].EffectPrefab[i].isRandomRange, CoreMovement.FancingDirection);
                            }
                        }
                        #endregion

                        //AudioClip
                        #region AudioClip
                        if (hitActions[currentHitBoxIndex].audioClip != null)
                        {
                            for (int i = 0; i < hitActions[currentHitBoxIndex].audioClip.Length; i++)
                            {
                                CoreSoundEffect.AudioSpawn(hitActions[currentHitBoxIndex].audioClip[i]);
                            }
                        }
                        #endregion

                    }//ShakeCam
                    #region ShakeCam
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
                    #endregion
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
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage,
                                hitActions[currentHitBoxIndex].RepeatAction
                            );
                            continue;
                        }

                        switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                        {
                            case ENEMY_Size.Small:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                                hitActions[currentHitBoxIndex].RepeatAction
                            );
                                Debug.Log("Enemy Type Small, Normal Dam = " +
                                    CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                                break;
                            case ENEMY_Size.Medium:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage),
                                hitActions[currentHitBoxIndex].RepeatAction
                                );
                                break;
                            case ENEMY_Size.Big:
                                _damageable.Damage
                            (
                                CoreUnitStats.CalculStatsData,
                                coll.GetComponentInParent<Unit>().Core.CoreUnitStats.CalculStatsData,
                                (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                                hitActions[currentHitBoxIndex].RepeatAction
                            );

                                Debug.Log("Enemy Type Big, Normal Dam = " +
                                    CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex]
                                    + " Enemy_Size_WeakPer Additional Dam = " +
                                    (CoreUnitStats.DefaultPower + hitActions[currentHitBoxIndex].AdditionalDamage) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                                    );
                                break;
                        }
                    }
                    #endregion
                }

                //KnockBack
                #region KnockBack
                if (coll.TryGetComponent(out IKnockBackable knockbackables) && hitActions[currentHitBoxIndex].KnockbackAngle.magnitude > 0)
                {
                    knockbackables.KnockBack(hitActions[currentHitBoxIndex].KnockbackAngle, hitActions[currentHitBoxIndex].KnockbackAngle.magnitude, CoreMovement.FancingDirection);
                }
                #endregion

            }
            #endregion

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


            isTriggerOn = true;
        }
        private void CheckRushActionRect(AttackActionHitBox actionData)
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


            isTriggerOn = true;
        }

        protected override void Start()
        {
            base.Start();
            eventHandler.OnAttackAction -= HandleAttackAction;
            eventHandler.OnAttackAction += HandleAttackAction;
            eventHandler.OnAttackAction -= HandleAction;
            eventHandler.OnAttackAction += HandleAction;

            eventHandler.OnActionRectOn -= HandleActionRectOn;
            eventHandler.OnActionRectOn += HandleActionRectOn;
            eventHandler.OnActionRectOn -= HandleAction;
            eventHandler.OnActionRectOn += HandleAction;

            eventHandler.OnActionRectOff -= HandleActionRectOff;
            eventHandler.OnActionRectOff += HandleActionRectOff;

            eventHandler.OnRushActionRectOn -= HandleRushActionRectOn;
            eventHandler.OnRushActionRectOn += HandleRushActionRectOn;
            eventHandler.OnRushActionRectOn -= HandleAction;
            eventHandler.OnRushActionRectOn += HandleAction;

            eventHandler.OnRushActionRectOff -= HandleRushActionRectOff;
            eventHandler.OnRushActionRectOff += HandleRushActionRectOff;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            eventHandler.OnAttackAction -= HandleAttackAction;

            eventHandler.OnAttackAction -= HandleAction;

            eventHandler.OnActionRectOn -= HandleActionRectOn;

            eventHandler.OnActionRectOn -= HandleAction;

            eventHandler.OnActionRectOff -= HandleActionRectOff;

            eventHandler.OnRushActionRectOn -= HandleRushActionRectOn;

            eventHandler.OnRushActionRectOff -= HandleRushActionRectOff;
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
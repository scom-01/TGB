using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SOB.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData, ActionDamage>
    {
        private ActionHitBox hitBox;

        protected override void HandleEnter()
        {
            base.HandleEnter();
        }
        /// <summary>
        /// Hit Collider가 감지된 오브젝트에 Damage
        /// </summary>
        /// <param name="coll"></param>
        private void HandleDetectedCollider2D(Collider2D[] coll)
        {
            if (currentActionData != null)
            {
                CheckAttackAction(currentActionData, coll);
            }
            //if (currentActionData != null && currentAirActionData != null)
            //{
            //    if (weapon.InAir)
            //    {
            //        CheckAttackAction(currentAirActionData, coll);
            //    }
            //    else
            //    {
            //        CheckAttackAction(currentActionData, coll);
            //    }
            //}
            //else if (currentActionData == null)
            //{
            //    CheckAttackAction(currentAirActionData, coll);
            //}
            //else if (currentAirActionData == null)
            //{
            //    CheckAttackAction(currentActionData, coll);
            //}
        }

        private void CheckAttackAction(ActionDamage actionDamage, Collider2D[] coll)
        {
            if (actionDamage == null)
                return;

            var currDamage = actionDamage.AdditionalDamage;
            if (currDamage.Length <= 0)
                return;

            if (currDamage.Length <= hitBox.currentHitBoxIndex)
                return;

            foreach (var item in coll)
            {
                if (item.gameObject.tag == this.gameObject.tag)
                    continue;

                if (item.TryGetComponent(out IDamageable damageable))
                {
                    #region 타입계산
                    //플레이어는 EnemyType이 없기때문에 생략
                    if (item.gameObject.tag == "Player")
                    {
                        damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]
                        );
                        continue;
                    }

                    switch (item.GetComponentInParent<Enemy>().enemyData.enemy_size)
                    {
                        case ENEMY_Size.Small:
                            damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]) * (1.0f + GlobalValue.Enemy_Size_WeakPer)
                        );
                            Debug.Log("Enemy Type Small, Normal Dam = " +
                                CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                            break;
                        case ENEMY_Size.Medium:
                            damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex])
                        );
                            break;
                        case ENEMY_Size.Big:
                            damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                        );

                            Debug.Log("Enemy Type Big, Normal Dam = " +
                                CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                            break;
                    }
                    #endregion
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDestory()
        {
            base.OnDestory();

            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}

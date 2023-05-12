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
        /// <param name="colls"></param>
        private void HandleDetectedCollider2D(Collider2D[] colls)
        {
            if (currentActionData != null)
            {
                CheckAttackAction(currentActionData, colls);
            }
        }

        private void CheckAttackAction(ActionDamage actionDamage, Collider2D[] colls)
        {
            if (actionDamage == null)
                return;

            var currDamage = actionDamage.AdditionalDamage;
            if (currDamage.Length <= 0)
                return;

            if (currDamage.Length <= hitBox.currentHitBoxIndex)
                return;

            foreach (var coll in colls)
            {
                if (coll.gameObject.tag == this.gameObject.tag)
                    continue;

                if (coll.TryGetComponent(out IDamageable damageable))
                {
                    #region 타입계산
                    //플레이어는 EnemyType이 없기때문에 생략
                    if (coll.gameObject.tag == "Player")
                    {
                        damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex]
                        );
                        continue;
                    }

                    switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                    {
                        case ENEMY_Size.Small:
                            damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
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
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (CoreUnitStats.StatsData.DefaultPower + currDamage[hitBox.currentHitBoxIndex])
                        );
                            break;
                        case ENEMY_Size.Big:
                            damageable.Damage
                        (
                            CoreUnitStats.StatsData,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
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

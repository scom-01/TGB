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
        /// <summary>
        /// Hit Collider가 감지된 오브젝트에 Damage
        /// </summary>
        /// <param name="coll"></param>
        private void HandleDetectedCollider2D(Collider2D[] coll)
        {
            foreach (var item in coll)
            {
                if (item.gameObject.tag == this.gameObject.tag)
                    continue;

                print($"Detected {item.name}");
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    #region 타입계산
                    //플레이어는 EnemyType이 없기때문에 생략
                    if (item.gameObject.tag == "Player")
                    {
                        damageable.Damage
                        (
                            this.GetComponentInParent<Unit>().UnitData.statsStats,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            GetComponent<Weapon>().weaponData.EffectPrefab,
                            this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount
                        );
                        continue;
                    }

                    switch (item.GetComponentInParent<Enemy>().enemyData.enemy_size)
                    {
                        case ENEMY_Size.Small:
                            damageable.Damage
                        (
                            this.GetComponentInParent<Unit>().UnitData.statsStats,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            GetComponent<Weapon>().weaponData.EffectPrefab,
                            (this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount) * (1.0f + GlobalValue.Enemy_Size_WeakPer)
                        );
                            Debug.Log("Enemy Type Small, Normal Dam = " +
                                this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                            break;
                        case ENEMY_Size.Medium:
                            damageable.Damage
                        (
                            this.GetComponentInParent<Unit>().UnitData.statsStats,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            GetComponent<Weapon>().weaponData.EffectPrefab,
                            (this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount)
                        );
                            break;
                        case ENEMY_Size.Big:
                            damageable.Damage
                        (
                            this.GetComponentInParent<Unit>().UnitData.statsStats,
                            item.GetComponentInParent<Unit>().UnitData.statsStats,
                            GetComponent<Weapon>().weaponData.EffectPrefab,
                            (this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                        );

                            Debug.Log("Enemy Type Big, Normal Dam = "+ 
                                this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount 
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (this.GetComponentInParent<Unit>().Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + this.currentActionData.Amount) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
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

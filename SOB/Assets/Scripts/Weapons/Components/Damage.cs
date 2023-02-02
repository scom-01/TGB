using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SOB.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData,AttackDamage>
    {
        private ActionHitBox hitBox;
        /// <summary>
        /// Hit Collider가 감지된 오브젝트에 Damage
        /// </summary>
        /// <param name="coll"></param>
        private void HandleDetectedCollider2D(Collider2D[] coll)
        {
            foreach(var item in coll)
            {
                if (item.gameObject.tag == "Player")
                    continue;

                print($"Detected Item {item.name}");
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(this.GetComponentInParent<Unit>().UnitData.commonStats,item.GetComponentInParent<Unit>().UnitData.commonStats,GetComponent<Weapon>().weaponData.EffectPrefab, this.GetComponentInParent<Unit>().UnitData.commonStats.DefaulPower);
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

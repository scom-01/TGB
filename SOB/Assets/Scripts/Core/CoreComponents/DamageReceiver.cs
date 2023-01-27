using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;

        private CoreComp<UnitStats> stats;
        private CoreComp<ParticleManager> particleManager;
        public void Damage(GameObject attacker, GameObject victim, ElementalPower elementalPower, DamageAttiribute attiribute, float amount)
        {
            //attacker.GetComponentInChildren<Core>().GetCoreComponent<UnitStats>()
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            stats.Comp?.DecreaseHealth(elementalPower, attiribute, amount);            
            if (damageParticles == null)
            {
                Debug.Log("Combat DamageParticles is Null");
                return;
            }
            particleManager.Comp?.StartParticlesWithRandomRotation(damageParticles);
        }
        public void Damage(CommonData AttackterCommonData, CommonData VictimCommonData, GameObject EffectPrefab, float amount)
        {
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            stats.Comp?.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            if (EffectPrefab == null)
            {
                Debug.Log("Combat DamageParticles is Null");
                return;
            }
            particleManager.Comp?.StartParticlesWithRandomPosition(EffectPrefab, 1.5f);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<UnitStats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}
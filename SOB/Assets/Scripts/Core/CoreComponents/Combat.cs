using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace SOB.CoreSystem
{
    public class Combat : CoreComponent, IDamageable, IKnockbackable
    {
        [SerializeField] private GameObject damageParticles;

        private Movement Movement 
        {
            get => movement ?? core.GetCoreComponent(ref movement); 
        }
        private CollisionSenses CollisionSenses
        {
            get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
        }
        private UnitStats Stats 
        { 
            get => stats ?? core.GetCoreComponent(ref stats); 
        }
        //private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

        private Movement movement;
        private CollisionSenses collisionSenses;
        private UnitStats stats;
        //private ParticleManager particleManager;

        [SerializeField] private float maxKnockbackTime = 0.2f;

        private bool isKnockbackActive;
        private float knockbackStartTime;

        public override void LogicUpdate()
        {
            CheckKnockback();
        }

        public void Damage(GameObject attacker, GameObject victim, ElementalPower elementalPower, DamageAttiribute attiribute, float amount)
        {
            //attacker.GetComponentInChildren<Core>().GetCoreComponent<UnitStats>()
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            Stats?.DecreaseHealth(elementalPower, attiribute, amount);
            //ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
        }
        public void Damage(CommonData AttackterCommonData, CommonData VictimCommonData, float amount)
        {
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            Stats?.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            //ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
        }
        public void Knockback(Vector2 angle, float strength, int direction)
        {
            Movement?.SetVelocity(strength, angle, direction);
            Movement.CanSetVelocity = false;
            isKnockbackActive = true;
            knockbackStartTime = Time.time;
        }
        private void CheckKnockback()
        {
            if (isKnockbackActive
                && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.GroundCheck)
                    || Time.time >= knockbackStartTime + maxKnockbackTime)
               )
            {
                isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}
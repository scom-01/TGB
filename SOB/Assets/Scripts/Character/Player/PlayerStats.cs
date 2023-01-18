using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class PlayerStats : CoreComponent
    {
        public event Action OnHealthZero;

        [SerializeField]
        private float maxHealth;
        private float currentHealth;

        [SerializeField]
        private GameObject
            deathChunkParticle,
            deathBloodParticle;

        private GameManager GM;

        protected override void Awake()
        {
            base.Awake();
            currentHealth = maxHealth;
        }        

        public void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0.0f)
            {                
                OnHealthZero?.Invoke();
            }
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }

        private void Die()
        {
            if (deathChunkParticle != null)
                Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
            if (deathBloodParticle != null)
                Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
            GM.Respawn();
            Destroy(gameObject);
        }

    }
}
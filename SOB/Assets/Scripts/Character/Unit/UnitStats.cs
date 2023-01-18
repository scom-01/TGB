using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class UnitStats : CoreComponent
    {
        public event Action OnHealthZero;

        [SerializeField]
        private float maxHealth;
        private float currentHealth;

        [SerializeField]
        private GameObject
            deathChunkParticle,
            deathBloodParticle;

        protected override void Awake()
        {
            base.Awake();
            currentHealth = maxHealth;            
        }        

        public void IncreaseMaxHealth(float amount)
        {
            if (core != null)
            {
                maxHealth += amount;
                currentHealth += amount;
            }
            else
            {
                Debug.LogWarning($"{transform.parent.name} is Core null");
            }
        }

        public void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (currentHealth <= 0.0f)
            {
                currentHealth = 0.0f;
                OnHealthZero?.Invoke();
            }
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }

    }
}
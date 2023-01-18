using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField]
        private GameObject[] deathParticles;
        //private ParticleManager ParticleManager => ParticleManager ? particleManager : core.GetCoreComponent(ref particleManager);
        //private ParticleManager particleManager;

        private UnitStats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private UnitStats stats;

        
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                //ParticleManager.StartParticles(particle);
            }

            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Stats.OnHealthZero += Die;
        }

        private void OnDisable()
        {
            Stats.OnHealthZero -= Die;
        }
    }
}

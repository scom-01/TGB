using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

namespace SOB.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField]
        private GameObject[] deathParticles;
        private ParticleManager ParticleManager
        {
            get
            {
                if(particleManager == null)
                {
                    core.GetCoreComponent(ref particleManager);
                }
                return particleManager;
            }
        }
            
        private ParticleManager particleManager;


        private UnitStats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private UnitStats stats;

        
        public void Die()
        {
            if(core.Unit.GetType() != typeof(Player))
            {
                GameManager.Inst.StageManager.SPM.UIEnemyCount--;
            }
            foreach (var particle in deathParticles)
            {
                var particleObject = ParticleManager.StartParticlesWithRandomPos(particle,0.5f);
                particleObject.GetComponent<Animator>().speed = Random.Range(0.3f, 1f);
            }
            var item = core.Unit.Inventory.items;
            int count = item.Count;
            for(int i = 0; i < count; i++)
            {
                core.Unit.Inventory.RemoveInventoryItem(item[0]);
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

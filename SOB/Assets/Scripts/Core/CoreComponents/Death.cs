using UnityEngine;

namespace SOB.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField]
        protected GameObject[] deathParticles;
        protected ParticleManager ParticleManager
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


        protected UnitStats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private UnitStats stats;

        [HideInInspector] public bool isDead = false;
        
        public virtual void Die()
        {
            if (isDead)
                return;

            isDead = true;
            foreach (var particle in deathParticles)
            {
                var particleObject = ParticleManager.StartParticlesWithRandomPos(particle,0.5f);
                particleObject.GetComponent<Animator>().speed = Random.Range(0.3f, 1f);
            }
            var item = core.Unit.Inventory.items;
            int count = item.Count;
            for(int i = 0; i < count; i++)
            {
                core.Unit.Inventory.RemoveInventoryItem(item[i]);
            }
            core.Unit.DieEffect();
            //core.transform.parent.gameObject.SetActive(false);
        }

        protected void OnEnable()
        {
            Stats.OnHealthZero += Die;
        }

        protected void OnDisable()
        {
            Stats.OnHealthZero -= Die;
        }
    }
}

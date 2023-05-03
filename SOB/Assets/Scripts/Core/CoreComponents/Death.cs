using UnityEngine;

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

        [HideInInspector] public bool isDead = false;
        
        public void Die()
        {
            if (isDead)
                return;

            if(core.Unit.GetType() != typeof(Player))
            {
                GameManager.Inst.StageManager.SPM.UIEnemyCount--;
            }
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
                core.Unit.Inventory.RemoveInventoryItem(item[0]);
            }
            core.Unit.DieEffect();
            //core.transform.parent.gameObject.SetActive(false);
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

using UnityEngine;

namespace SOB.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField]
        protected GameObject[] deathChunk;
        protected EffectManager EffectManager
        {
            get
            {
                if(effectManager == null)
                {
                    core.GetCoreComponent(ref effectManager);
                }
                return effectManager;
            }
        }
            
        private EffectManager effectManager;


        protected UnitStats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private UnitStats stats;

        [HideInInspector] public bool isDead = false;
        
        public virtual void Die()
        {
            if (isDead)
                return;

            isDead = true;
            foreach (var effect in deathChunk)
            {
                var particleObject = EffectManager.StartChunkEffectsWithRandomPos(effect, 0.5f);
                particleObject.GetComponent<Animator>().speed = Random.Range(0.3f, 1f);
            }
            var item = core.Unit.Inventory.items;
            int count = item.Count;
            for(int i = 0; i < count; i++)
            {
                core.Unit.Inventory.RemoveInventoryItem(item[i]);
            }
            core.Unit.DieEffect();
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

using UnityEngine;

namespace SOB.CoreSystem
{

    public class ParticleManager : CoreComponent
    {
        private Transform particleContainer;

        protected override void Awake()
        {
            base.Awake();
            if (GameObject.FindGameObjectWithTag("ParticleContainer").transform != null)
            {
                particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
            }
        }

        public GameObject StartParticles(GameObject particlePrefab, Vector2 pos, Quaternion rot)
        {
            return Instantiate(particlePrefab, pos, rot, particleContainer);
        }

        public GameObject StartParticles(GameObject particlePrefab)
        {
            return Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }

        public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartParticles(particlePrefab, transform.position, randomRotation);
        }

        public GameObject StartParticlesWithRandomPosition(GameObject particlePrefab, float Range)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartParticles(particlePrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation);
        }
    }
}
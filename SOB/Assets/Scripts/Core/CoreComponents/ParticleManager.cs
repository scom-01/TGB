using Cinemachine;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class ParticleManager : CoreComponent
    {
        [TagField]
        [field: SerializeField] private string particleContainerTagName;
        private Transform particleContainer;

        protected override void Awake()
        {
            base.Awake();
            if (GameObject.FindGameObjectWithTag(particleContainerTagName).transform != null)
            {
                particleContainer = GameObject.FindGameObjectWithTag(particleContainerTagName).transform;
            }
        }

        public GameObject StartParticles(GameObject particlePrefab, Vector2 pos, Quaternion rot)
        {
            return Instantiate(particlePrefab, pos, rot, particleContainer);
        }
        public GameObject StartParticles(GameObject particlePrefab, Vector2 pos)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                return Instantiate(particlePrefab, pos, Quaternion.identity, particleContainer);
            return Instantiate(particlePrefab, pos, Quaternion.Euler(0f, 180.0f, 0f), particleContainer);
        }

        public GameObject StartParticles(GameObject particlePrefab)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return Instantiate(particlePrefab, transform.position, Quaternion.identity, particleContainer);
            }
            return Instantiate(particlePrefab, transform.position, Quaternion.Euler(0f, 180.0f, 0f), particleContainer);
        }

        public GameObject StartParticlesWithRandomRot(GameObject particlePrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartParticles(particlePrefab, transform.position, randomRotation);
        }

        public GameObject StartParticlesWithRandomPos(GameObject particlePrefab, float Range)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return StartParticles(particlePrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.identity);
            }

            return StartParticles(particlePrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.Euler(0f, 180.0f, 0f));
        }
        public GameObject StartParticlesWithRandomPosRot(GameObject particlePrefab, float Range)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartParticles(particlePrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation);
        }
    }
}
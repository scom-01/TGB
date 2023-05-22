using Cinemachine;
using System.Data;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace SOB.CoreSystem
{
    public class EffectManager : CoreComponent
    {
        [TagField]
        [field: SerializeField] private string effectContainerTagName;
        private Transform effectContainerTransform;
        private EffectContainer effectContainer;

        protected override void Awake()
        {
            base.Awake();
            if (GameObject.FindGameObjectWithTag(effectContainerTagName).transform != null)
            {
                effectContainer = GameObject.FindGameObjectWithTag(effectContainerTagName).GetComponent<EffectContainer>();                
                effectContainerTransform = GameObject.FindGameObjectWithTag(effectContainerTagName).transform;
                if (effectContainer == null)
                {
                    effectContainer = effectContainerTransform.gameObject.AddComponent<EffectContainer>();
                }
            }
        }

        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Quaternion rot)
        {
            return effectContainer.AddEffectList(effectPrefab, pos, rot, effectContainerTransform);
        }
        public GameObject StartChunkEffects(GameObject effectPrefab, Vector2 pos, Quaternion rot)
        {            
            return Instantiate(effectPrefab, pos, rot, effectContainerTransform);
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
            }
            return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.Euler(0f, 180.0f, 0f), effectContainerTransform);
        }
        public GameObject StartChunkEffects(GameObject effectPrefab, Vector2 pos)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return Instantiate(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
            }
            return Instantiate(effectPrefab, pos, Quaternion.Euler(0f, 180.0f, 0f), effectContainerTransform);
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Vector3 euler)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
            }
            return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y, euler.z), effectContainerTransform);
        }
        public GameObject StartChunkEffects(GameObject effectPrefab, Vector2 pos, Vector3 euler)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return Instantiate(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
            }
            return Instantiate(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y, euler.z), effectContainerTransform);
        }

        public GameObject StartEffects(GameObject effectPrefab)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return effectContainer.AddEffectList(effectPrefab, transform.position, Quaternion.identity, effectContainerTransform);
            }
            return effectContainer.AddEffectList(effectPrefab, transform.position, Quaternion.Euler(0f, 180.0f, 0f), effectContainerTransform);
        }
        public GameObject StartChunkEffects(GameObject effectPrefab)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return Instantiate(effectPrefab, transform.position, Quaternion.identity, effectContainerTransform);
            }
            return Instantiate(effectPrefab, transform.position, Quaternion.Euler(0f, 180.0f, 0f), effectContainerTransform);
        }

        public GameObject StartEffectsWithRandomRot(GameObject effectPrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, transform.position, randomRotation);
        }

        public GameObject StartEffectsWithRandomPos(GameObject effectPrefab, float Range)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.identity);
            }

            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.Euler(0f, 180.0f, 0f));
        }
        public GameObject StartChunkEffectsWithRandomPos(GameObject effectPrefab, float Range)
        {
            if (core.GetCoreComponent<Movement>().FancingDirection > 0)
            {
                return StartChunkEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.identity);
            }

            return StartChunkEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.Euler(0f, 180.0f, 0f));
        }
        public GameObject StartEffectsWithRandomPosRot(GameObject effectPrefab, float Range)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation);
        }
        public GameObject StartChunkEffectsWithRandomPosRot(GameObject effectPrefab, float Range)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartChunkEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation);
        }
    }
}
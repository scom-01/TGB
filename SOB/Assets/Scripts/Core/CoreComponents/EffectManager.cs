using Cinemachine;
using System.Data;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class EffectManager : CoreComponent
    {
        [TagField]
        [field: SerializeField] private string effectContainerTagName = "EffectContainer";
        private Transform effectContainerTransform;
        private EffectContainer effectContainer;

        protected override void Awake()
        {
            base.Awake();
            if (effectContainerTagName == "")
            {
                effectContainerTagName = "EffectContainer";
            }
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
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                return Instantiate(effectPrefab, pos, rot, effectContainerTransform);
            }
            else
            {
                return effectContainer.AddEffectList(effectPrefab, pos, rot, effectContainerTransform);
            }
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos)
        {
            if(effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if(effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
                }
                return Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, 180.0f, effectPrefab.transform.eulerAngles.z), effectContainerTransform);
            }
            else
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
                }
                return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, 180.0f, effectPrefab.transform.eulerAngles.z), effectContainerTransform);
            }
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Vector3 euler)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
                }
                return Instantiate(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y, euler.z), effectContainerTransform);
            }
            else
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.identity, effectContainerTransform);
                }
                return effectContainer.AddEffectList(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y, euler.z), effectContainerTransform);
            }
        }

        public GameObject StartEffects(GameObject effectPrefab)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles), effectContainerTransform);
                }
                return Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, 180.0f, effectPrefab.transform.eulerAngles.z), effectContainerTransform);
            }
            else
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return effectContainer.AddEffectList(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles), effectContainerTransform);
                }
                return effectContainer.AddEffectList(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, 180.0f, effectPrefab.transform.eulerAngles.z), effectContainerTransform);
            }
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
        public GameObject StartEffectsWithRandomPos(GameObject effectPrefab, float Range, int FancingDirection)
        {
            if (FancingDirection > 0)
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
       
        public GameObject StartEffectsWithRandomPosRot(GameObject effectPrefab, float Range)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation);
        }
    }
}
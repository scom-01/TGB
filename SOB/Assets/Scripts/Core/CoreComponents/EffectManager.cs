using Cinemachine;
using System.Data;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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

        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Quaternion rot, bool _isFollow = false)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                return Instantiate(effectPrefab, pos, rot, _isFollow ? this.transform : effectContainerTransform);
            }
            else
            {
                return effectContainer.CheckEffect(effectPrefab, _isFollow ? this.transform : effectContainerTransform).GetObejct(pos, rot);
            }
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, bool _isFollow = false)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles), _isFollow ? this.transform : effectContainerTransform);
                }
                return Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), _isFollow ? this.transform : effectContainerTransform);
            }
            else
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return effectContainer.CheckEffect(effectPrefab, _isFollow ? this.transform : effectContainerTransform).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles));
                }
                return effectContainer.CheckEffect(effectPrefab, _isFollow ? this.transform : effectContainerTransform).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
            }
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Vector3 euler, bool _isFollow = false)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, pos, Quaternion.Euler(euler), _isFollow ? this.transform : effectContainerTransform);
                }
                return Instantiate(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z), effectContainerTransform);
            }
            else
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return effectContainer.CheckEffect(effectPrefab, _isFollow ? this.transform : effectContainerTransform).GetObejct(pos, Quaternion.Euler(euler));
                }
                return effectContainer.CheckEffect(effectPrefab, _isFollow ? this.transform : effectContainerTransform).GetObejct(pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z));
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
                return Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), effectContainerTransform);
            }
            else
            {
                if (core.GetCoreComponent<Movement>().FancingDirection > 0)
                {
                    return effectContainer.CheckEffect(effectPrefab).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles));
                }
                return effectContainer.CheckEffect(effectPrefab).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
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
                                                    Quaternion.Euler(effectPrefab.transform.eulerAngles));
            }

            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
        }
        public GameObject StartEffectsWithRandomPos(GameObject effectPrefab, float Range, int FancingDirection, bool _isFollow = false)
        {
            if (FancingDirection > 0)
            {
                return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.Euler(effectPrefab.transform.eulerAngles), _isFollow);
            }

            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)),
                                                    Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), _isFollow);
        }

        public GameObject StartEffectsWithRandomPosRot(GameObject effectPrefab, float Range, bool _isFollow = false)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation, _isFollow);
        }
    }
}
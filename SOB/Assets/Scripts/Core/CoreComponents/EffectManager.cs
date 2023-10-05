using Cinemachine;
using SOB.Weapons.Components;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class EffectManager : CoreComponent
    {
        public List<ObjectPooling> ObjectPoolList = new List<ObjectPooling>();

        protected override void Awake()
        {
            base.Awake();            
        }

        #region Effects
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Quaternion rot, bool _isFollow = false)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                return Instantiate(effectPrefab, pos, rot, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
            }
            else
            {
                if(_isFollow)
                    return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(pos,rot);
                else
                    return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos, rot);
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
                if (core.CoreMovement.FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles), _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
                }
                return Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
            }
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    if (_isFollow)
                        return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles));
                    else
                        return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles));
                }
                if (_isFollow)
                    return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
                else
                    return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
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
                if (core.CoreMovement.FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, pos, Quaternion.Euler(euler), _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
                }
                return Instantiate(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z), GameManager.Inst.StageManager.EffectContainer.transform);
            }
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    if (_isFollow)
                        return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler));
                    else
                        return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler));
                }
                if (_isFollow)
                    return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z));
                else
                    return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z));
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
                if (core.CoreMovement.FancingDirection > 0)
                {
                    return Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles), GameManager.Inst.StageManager.EffectContainer.transform);
                }
                return Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), GameManager.Inst.StageManager.EffectContainer.transform);
            }
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    return (GameManager.Inst.StageManager?.EffectContainer.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles));
                }
                return (GameManager.Inst.StageManager?.EffectContainer.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab) as EffectPooling).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
            }
        }

        public GameObject StartEffectsWithRandomRot(GameObject effectPrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, transform.position, randomRotation);
        }

        public GameObject StartEffectsWithRandomPos(GameObject effectPrefab, float Range)
        {
            if (core.CoreMovement.FancingDirection > 0)
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

        public GameObject StartProjectileCheck(Unit _unit, ProjectileActionData _projectileActionData)
        {
            return GameManager.Inst.StageManager.EffectContainer?.CheckProjectile(_projectileActionData.Projectile).GetObejct(_unit, _projectileActionData.ProjectileData);
        }
        #endregion


        private ObjectPooling CheckObject(ObjectPooling_TYPE objectPooling_TYPE, GameObject _obj)
        {
            if (ObjectPoolList.Count == 0)
            {
                var obj = AddObject(objectPooling_TYPE, _obj, 5).GetComponent<EffectPooling>();
                return obj;
            }

            for (int i = 0; i < ObjectPoolList.Count; i++)
            {
                if (ObjectPoolList[i].Object == _obj)
                {
                    return ObjectPoolList[i];
                }
            }

            var newObj = AddObject(objectPooling_TYPE, _obj, 5).GetComponent<EffectPooling>();
            return newObj;
        }
        private GameObject AddObject(ObjectPooling_TYPE objectPooling_TYPE, GameObject _Obj, int amount)
        {
            var effectPool = Instantiate(GlobalValue.Base_EffectPooling, this.transform);
            effectPool.GetComponent<EffectPooling>().Init(_Obj, amount);
            ObjectPoolList.Add(effectPool.GetComponent<EffectPooling>());
            return effectPool;            
        }
    }
}
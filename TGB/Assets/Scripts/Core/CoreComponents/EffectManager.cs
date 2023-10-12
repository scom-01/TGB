using Cinemachine;
using TGB.Weapons.Components;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using System.Drawing;

namespace TGB.CoreSystem
{
    public class EffectManager : CoreComponent
    {
        public List<ObjectPooling> ObjectPoolList = new List<ObjectPooling>();

        protected override void Awake()
        {
            base.Awake();
        }

        #region Effects
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Quaternion rot, bool _isFollow = false, float size = 0)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (size == 0)
                size = effectPrefab.gameObject.transform.localScale.x;

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                GameObject go = Instantiate(effectPrefab, pos, rot, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
                go.transform.localScale = new Vector3(size, size, size);
                return go;
            }
            else
            {
                if (_isFollow)
                    return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, size) as EffectPooling).GetObejct(pos, rot, size);
                else
                    return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, GameManager.Inst.StageManager.EffectContainer.transform, size) as EffectPooling).GetObejct(pos, rot, size);
            }
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, bool _isFollow = false, float size = 0)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (size == 0)
                size = effectPrefab.gameObject.transform.localScale.x;

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    GameObject gameObject = Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles), _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
                    gameObject.transform.localScale = new Vector3(size, size, size);
                    return gameObject;
                }
                GameObject gameObject1 = Instantiate(effectPrefab, pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
                gameObject1.transform.localScale = new Vector3(size, size, size);
                return gameObject1;
            }
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    if (_isFollow)
                        return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles), size);
                    else
                        return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles), size);
                }
                if (_isFollow)
                    return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), size);
                else
                    return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), size);
            }
        }
        public GameObject StartEffects(GameObject effectPrefab, Vector2 pos, Vector3 euler, bool _isFollow = false, float size = 0)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (size == 0)
                size = effectPrefab.gameObject.transform.localScale.x;

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    GameObject go = Instantiate(effectPrefab, pos, Quaternion.Euler(euler), _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform);
                    go.transform.localScale = new Vector3(size, size, size);
                    return go;
                }
                GameObject go1 = Instantiate(effectPrefab, pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z), GameManager.Inst.StageManager.EffectContainer.transform);
                go1.transform.localScale = new Vector3(size, size, size);
                return go1;
            }
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    if (_isFollow)
                        return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler), size);
                    else
                        return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler), size);
                }
                if (_isFollow)
                    return (CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z), size);
                else
                    return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, _isFollow ? this.transform : GameManager.Inst.StageManager.EffectContainer.transform, size) as EffectPooling).GetObejct(pos, Quaternion.Euler(euler.x, euler.y + 180f, euler.z), size);
            }
        }

        public GameObject StartEffects(GameObject effectPrefab, float size = 0)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (size == 0)
                size = effectPrefab.gameObject.transform.localScale.x;

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    GameObject go = Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles), GameManager.Inst.StageManager.EffectContainer.transform);
                    go.transform.localScale =new Vector3(size, size, size);
                    return go;
                }
                GameObject go1 = Instantiate(effectPrefab, transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), GameManager.Inst.StageManager.EffectContainer.transform);
                go1.transform.localScale = new Vector3(size, size, size);
                return go1;
            }
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    return (GameManager.Inst.StageManager?.EffectContainer.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, null, size) as EffectPooling).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles), size);
                }
                return (GameManager.Inst.StageManager?.EffectContainer.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, null, size) as EffectPooling).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z), size);
            }
        }

        public GameObject StartEffectsWithRandomRot(GameObject effectPrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, transform.position, randomRotation);
        }

        public GameObject StartEffectsWithRandomPos(GameObject effectPrefab, float Range)
        {
            if (effectPrefab.GetComponent<EffectController>() == null)
            {
                effectPrefab.AddComponent<EffectController>();
            }

            if (effectPrefab.GetComponent<EffectController>().isDestroy)
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
            else
            {
                if (core.CoreMovement.FancingDirection > 0)
                {
                    return (GameManager.Inst.StageManager?.EffectContainer.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, null) as EffectPooling).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles));
                }
                else
                {
                    return (GameManager.Inst.StageManager?.EffectContainer.CheckObject(ObjectPooling_TYPE.Effect, effectPrefab, null) as EffectPooling).GetObejct(transform.position, Quaternion.Euler(effectPrefab.transform.eulerAngles.x, effectPrefab.transform.eulerAngles.y + 180.0f, effectPrefab.transform.eulerAngles.z));
                }
            }
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

        public GameObject StartEffectsWithRandomPosRot(GameObject effectPrefab, float Range, bool _isFollow = false, float size = 0)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
            return StartEffects(effectPrefab, new Vector2(
                                                    transform.position.x + Random.Range(-Range, Range),
                                                    transform.position.y + Random.Range(-Range, Range)), randomRotation, _isFollow, size);
        }

        public GameObject StartProjectileCheck(Unit _unit, ProjectileActionData _projectileActionData)
        {
            return GameManager.Inst.StageManager.EffectContainer?.CheckProjectile(_projectileActionData.Projectile).GetObejct(_unit, _projectileActionData.ProjectileData);
        }
        #endregion


        private ObjectPooling CheckObject(ObjectPooling_TYPE objectPooling_TYPE, GameObject _obj, float size = 0)
        {
            if (ObjectPoolList.Count == 0)
            {
                var obj = AddObject(objectPooling_TYPE, _obj, 5, size).GetComponent<EffectPooling>();
                return obj;
            }

            for (int i = 0; i < ObjectPoolList.Count; i++)
            {
                if (ObjectPoolList[i].Object == _obj)
                {
                    return ObjectPoolList[i];
                }
            }

            var newObj = AddObject(objectPooling_TYPE, _obj, 5, size).GetComponent<EffectPooling>();
            return newObj;
        }
        private GameObject AddObject(ObjectPooling_TYPE objectPooling_TYPE, GameObject _Obj, int amount, float size = 0)
        {
            var effectPool = Instantiate(GlobalValue.Base_EffectPooling, this.transform);
            effectPool.GetComponent<EffectPooling>().Init(_Obj, amount, size);
            ObjectPoolList.Add(effectPool.GetComponent<EffectPooling>());
            return effectPool;
        }
    }
}
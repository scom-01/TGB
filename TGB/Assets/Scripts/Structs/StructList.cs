using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace TGB
{
    [Serializable]
    public struct AudioPrefab
    {
        public AudioClip Clip;
        [Range(0,1)]
        public float Volume;
        public AudioPrefab(AudioClip clip, float volume)
        {
            Clip = clip;
            Volume = volume;
        }
    }

    [Serializable]
    public struct EffectPrefab
    {
        [Tooltip("유닛의 바닥에서 스폰")]
        /// <summary>
        /// Spawn Pos isGrounded
        /// </summary>
        public bool isGround;
        [Tooltip("유닛의 머리에서 스폰")]
        /// <summary>
        /// Spawn Pos isGrounded
        /// </summary>
        public bool isHeader;
        [Tooltip("스폰 위치를 유닛 기준이 아닌 오브젝트의 위치로 스폰")]
        public bool isTransformGlobal;
        [Tooltip("랜덤 위치 스폰")]
        /// <summary>
        /// Spawn Pos isRandom
        /// </summary>
        public bool isRandomPosRot;
        [Tooltip("랜덤 위치 범위")]
        /// <summary>
        /// Spawn Pos RandomRange
        /// </summary>
        public float isRandomRange;
        [Tooltip("이펙트의 Offset")]
        /// <summary>
        /// Effect Offset
        /// </summary>
        public Vector2 EffectOffset;
        /// <summary>
        /// Additional Effect Scale
        /// </summary>
        [field: Tooltip("This Additional float , ex.1) default 1 + additionalScale , ex.2) EffectScale(1) = 2")]
        public Vector3 EffectScale;
        [Tooltip("유닛의 하위 오브젝트로 스폰")]
        /// <summary>
        /// isFollow Unit
        /// </summary>
        public bool isFollowing;
        [Tooltip("스폰할 이펙트 오브젝트")]
        /// <summary>
        /// Spawn Object
        /// </summary>
        public GameObject Object;

        public GameObject SpawnObject(Vector3 pos)
        {
            if (Object == null)
                return null;

            var offset = new Vector3(EffectOffset.x, EffectOffset.y);
            var size = EffectScale;

            if (isRandomPosRot)
            {
                var randomRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360f));
                pos = new Vector2(pos.x + UnityEngine.Random.Range(-isRandomRange, isRandomRange), pos.y + UnityEngine.Random.Range(-isRandomRange, isRandomRange));
                return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, Object, EffectScale, GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos, randomRotation, size);
            }
            else
            {
                return (GameManager.Inst.StageManager.EffectContainer?.CheckObject(ObjectPooling_TYPE.Effect, Object, EffectScale, GameManager.Inst.StageManager.EffectContainer.transform) as EffectPooling).GetObejct(pos + offset, Quaternion.Euler(Object.transform.eulerAngles), size);
            }            

            return null;
        }

        public GameObject SpawnObject(Unit unit)
        {
            if (Object == null)
                return null;

            if (unit == null)
                return null;

            var offset = new Vector3(EffectOffset.x * unit.Core.CoreMovement.FancingDirection, EffectOffset.y);
            var size = EffectScale;

            if (isTransformGlobal)
            {
                return unit.Core.CoreEffectManager.StartEffectsPos(Object, Vector2.zero, size, false);
            }

            if (isHeader)
            {
                return unit.Core.CoreEffectManager.StartEffectsPos(Object,
                    (isRandomPosRot ? unit.Core.CoreCollisionSenses.HeaderCenterPos : unit.Core.CoreCollisionSenses.HeaderCenterPos + offset), size, isFollowing);
            }
            else if (isGround)
            {
                return unit.Core.CoreEffectManager.StartEffectsPos(Object,
                    (isRandomPosRot ? unit.Core.CoreCollisionSenses.GroundCenterPos : unit.Core.CoreCollisionSenses.GroundCenterPos + offset), size, isFollowing);
            }
            else
            {
                if (isRandomPosRot)
                {
                    return unit.Core.CoreEffectManager.StartEffectsWithRandomPosRot(
                            Object,
                            isRandomRange, size, isFollowing);
                }
                else
                {
                    return unit.Core.CoreEffectManager.StartEffectsPos(Object, unit.Core.CoreCollisionSenses.UnitCenterPos + offset, size, isFollowing);
                }
            }
        }
    }
}

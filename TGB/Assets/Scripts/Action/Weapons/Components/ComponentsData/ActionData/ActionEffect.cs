using System;
using UnityEngine;

namespace TGB.Weapons.Components
{
    [Serializable]
    public class ActionEffect : ActionData
    {
        [field: SerializeField] public EffectPrefab[] EffectParticles { get; private set; }
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
    }
}

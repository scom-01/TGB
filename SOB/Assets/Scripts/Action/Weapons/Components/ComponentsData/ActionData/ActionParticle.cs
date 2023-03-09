using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionParticle : ActionData
    {
        [field: SerializeField] public EffectPrefab[] ParticlePrefabs { get; private set; }
    }

    [Serializable]
    public struct EffectPrefab
    {
        /// <summary>
        /// Spawn Pos isGrounded
        /// </summary>
        public bool isGround;
        /// <summary>
        /// Spawn Pos isRandom
        /// </summary>
        public bool isRandomPosRot;
        /// <summary>
        /// Spawn Pos RandomRange
        /// </summary>
        public float isRandomRange;
        /// <summary>
        /// Spawn Object
        /// </summary>
        public GameObject Object;
    }

}

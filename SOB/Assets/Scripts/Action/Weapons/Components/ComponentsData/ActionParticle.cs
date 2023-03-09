using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionParticle : ActionData
    {
        [field: SerializeField] public GameObject[] ParticlePrefabs { get; private set; }
    }
}

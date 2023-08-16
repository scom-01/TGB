using SOB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionShakeCam : ActionData
    {
        [field: SerializeField] public CamData[] ShakeCamData { get; private set; }
    }
    [Serializable]
    public struct CamData
    {
        [Range(0.1f, 5f)] public float Duration;
        [Range(0.01f, 0.1f)] public float Range;
        [Range(0.001f, 0.01f)] public float RepeatRate;
    }
}

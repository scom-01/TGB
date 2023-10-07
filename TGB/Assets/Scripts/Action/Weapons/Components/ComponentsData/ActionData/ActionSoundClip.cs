using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    [Serializable]
    public class ActionSoundClip : ActionData
    {
        [field: SerializeField] public AudioClip[] audioClips;
    }
}

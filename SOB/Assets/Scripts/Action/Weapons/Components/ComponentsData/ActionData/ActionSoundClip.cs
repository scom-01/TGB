using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionSoundClip : ActionData
    {
        [field: SerializeField] public AudioClipCommand[] audioClips;
        [Serializable]
        public struct AudioClipCommand
        {
            public AudioClip[] audioClips;
            public CommandEnum Command;
        }
    }
}

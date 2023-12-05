using System;
using UnityEngine;

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
}

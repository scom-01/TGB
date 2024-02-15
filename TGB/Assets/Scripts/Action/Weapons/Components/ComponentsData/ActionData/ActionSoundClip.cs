using System;
using UnityEngine;

namespace TGB.Weapons.Components
{
    [Serializable]
    public class ActionSoundClip : ActionData
    {
        [field: SerializeField] public AudioPrefab[] audioDataList;
    }
}

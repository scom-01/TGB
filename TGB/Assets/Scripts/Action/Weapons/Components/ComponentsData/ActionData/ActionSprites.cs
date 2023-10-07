using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    [Serializable]
    public class ActionSprites : ActionData
    {
        [field: SerializeField] public Sprite[] WeaponSprites { get; private set; }
    }
}

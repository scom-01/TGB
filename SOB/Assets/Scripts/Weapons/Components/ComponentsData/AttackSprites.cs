using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class AttackSprites : ActionData
    {
        [field: SerializeField] public Sprite[] GroundedSprites { get; private set; }
        [field: SerializeField] public Sprite[] InAirSprites { get; private set; }
    }
}

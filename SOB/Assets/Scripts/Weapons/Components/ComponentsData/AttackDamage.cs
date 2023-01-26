using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class AttackDamage : ActionData
    {
        [field : SerializeField] public float Amount { get; private set; }
    }
}

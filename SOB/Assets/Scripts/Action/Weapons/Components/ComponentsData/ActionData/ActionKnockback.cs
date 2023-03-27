using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionKnockback : ActionData
    {
        [field: SerializeField] public KnockbackData[] Knockback { get; private set; }

        [Serializable]
        public struct KnockbackData
        {
            public Vector2 KnockbackAngle;
            public CommandEnum Command;
        }
    }
}

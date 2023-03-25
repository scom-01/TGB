using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionDamage : ActionData
    {
        [field : SerializeField] public DamageAmount[] AdditionalDamageGround { get; private set; }
        [field : SerializeField] public DamageAmount[] AdditionalDamageInAir { get; private set; }

        [Serializable]
        public struct DamageAmount
        {
            public float Amount;
            public CommandEnum Command;
        }

    }
}

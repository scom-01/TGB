using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionDamage : ActionData
    {
        [field : SerializeField] public HitDamage[] HitDamage { get; private set; }
    }

    [Serializable]
    public struct HitDamage
    {
        [field: Tooltip("추가 데미지")]
        public float AdditionalDamage;
        [field: Tooltip("반복 횟수")]
        [field: Min(1)]
        public int RepeatAmount;
    }
}

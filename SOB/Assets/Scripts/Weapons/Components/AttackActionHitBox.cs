using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
        [field: SerializeField] public Vector2 KnockbackAngle { get; private set; }
    }
}

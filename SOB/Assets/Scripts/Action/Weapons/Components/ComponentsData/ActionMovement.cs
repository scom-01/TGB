using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionMovement : ActionData
    {
        [field: SerializeField] public bool CanFlip { get; private set; }
        [field: SerializeField] public bool CanMoveCtrl { get; private set; }
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public float Velocity { get; private set; }
    }
}
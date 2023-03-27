using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionMovement : ActionData
    {
        [field: SerializeField] public MovementCommandData[] movementCommands { get; private set; }

        [Serializable]
        public struct MovementCommandData
        {
            public bool CanFlip;
            public bool CanMoveCtrl ;
            public Vector2 Direction ;
            public float Velocity ;
            public CommandEnum Command ;
        }    
    }
}
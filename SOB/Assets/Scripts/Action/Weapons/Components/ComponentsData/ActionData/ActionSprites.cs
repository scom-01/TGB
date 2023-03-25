using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ActionSprites : ActionData
    {
        [field: SerializeField] public CommandActionSprites[] WeaponSprites { get; private set; }

        [Serializable]
        public struct CommandActionSprites
        {
            public Sprite[] sprites;
            public CommandEnum Command;
        }

    }
}

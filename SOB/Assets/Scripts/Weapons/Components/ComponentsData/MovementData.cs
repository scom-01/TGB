using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class MovementData : ComponentData<ActionMovement>
    {
        public MovementData()
        {
            ComponentDependency = typeof(WeaponMovement);
        }
    }
}

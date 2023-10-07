using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    public class KnockbackData : ComponentData<ActionKnockback>
    {
        public KnockbackData()
        {
            ComponentDependency = typeof(Knockback);
        }
    }
}

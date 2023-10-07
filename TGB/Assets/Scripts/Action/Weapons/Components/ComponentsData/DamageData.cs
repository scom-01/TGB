using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    public class DamageData : ComponentData<ActionDamage>
    {
        public DamageData()
        {
            ComponentDependency = typeof(Damage);
        }
    }
}

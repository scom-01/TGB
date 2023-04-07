using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class ShakeCamData : ComponentData<ActionShakeCam>
    {
        public ShakeCamData()
        {
            ComponentDependency = typeof(ShakeCam);
        }
    }
}

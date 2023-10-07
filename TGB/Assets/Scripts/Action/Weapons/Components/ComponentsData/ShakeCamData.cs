using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    public class ShakeCamData : ComponentData<ActionShakeCam>
    {
        public ShakeCamData()
        {
            ComponentDependency = typeof(ShakeCam);
        }
    }
}

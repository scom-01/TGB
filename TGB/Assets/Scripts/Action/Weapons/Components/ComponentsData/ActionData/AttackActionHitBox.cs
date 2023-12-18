using System;
using UnityEngine;

namespace TGB.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : ActionData
    {
        [field: SerializeField] public HitAction[] ActionHit;
    }
}

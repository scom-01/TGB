using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : ActionData
    {
        [field: SerializeField] public HitAction[] ActionHit;
    }

    [Serializable]
    public struct HitAction
    {
        public bool Debug;
        public Rect ActionRect;
        public EffectPrefab[] EffectPrefab;
        public AudioClip[] audioClip;
        public CamData[] camDatas;
    }
}

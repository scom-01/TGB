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
        /// <summary>
        /// 공격 범위
        /// </summary>
        public Rect ActionRect;
        [field : Min(1)]
        /// <summary>
        /// 다단 히트 횟수
        /// </summary>
        public int RepeatAction;
        /// <summary>
        /// 공격 시 효과
        /// </summary>
        public EffectPrefab[] EffectPrefab;
        /// <summary>
        /// 공격 시 사운드
        /// </summary>
        public AudioClip[] audioClip;
        /// <summary>
        /// 공격 시 ShakeCam
        /// </summary>
        public CamData[] camDatas;
    }
}

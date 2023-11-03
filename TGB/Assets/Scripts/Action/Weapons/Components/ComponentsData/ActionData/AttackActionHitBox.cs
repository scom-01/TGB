using System;
using UnityEngine;

namespace TGB.Weapons.Components
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

        [field: Min(1)]
        /// <summary>
        /// 다단 히트 횟수
        /// </summary>
        public int RepeatAction;

        /// <summary>
        /// 추가 데미지
        /// </summary>
        [field: Tooltip("추가 데미지")]
        public float AdditionalDamage;
        [field: Tooltip("고정 데미지 | AdditionalDamage로 고정데미지 피해")]
        public bool isFixed;
        /// <summary>
        /// Knockback Angle
        /// </summary>
        [field: Tooltip("넉백 Angle, 벡터 크기에 따라 넉백 증가")]
        public Vector2 KnockbackAngle;
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

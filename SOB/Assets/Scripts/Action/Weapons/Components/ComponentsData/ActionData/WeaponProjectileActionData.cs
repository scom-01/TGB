using SOB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ProjectileData
{
    /// <summary>
    /// 투사체의 스탯
    /// </summary>
    public StatsData Stats;
    /// <summary>
    /// 발사 위치
    /// </summary>
    public Vector3 Pos;
    /// <summary>
    /// 발사 각도
    /// </summary>
    public Vector3 Rot;
    /// <summary>
    /// CircleCollider Radius
    /// </summary>
    [Min(0.5f)]
    public float Radius;
    /// <summary>
    /// 중력 크기
    /// </summary>
    public float GravityScale;
    /// <summary>
    /// 발사 속도
    /// </summary>
    public float Speed;
    /// <summary>
    /// 싱글 히트인지 다단 히트인지 판별
    /// </summary>
    public bool isSingleShoot;
    /// <summary>
    /// 투사체 지속시간
    /// </summary>
    [Min(0.5f)]
    public float DurationTime;
    /// <summary>
    /// 투사체 Loop 이펙트
    /// </summary>
    public GameObject ProjectilePrefab;
    /// <summary>
    /// 투사체 피격 시 이펙트 
    /// </summary>
    public GameObject ImpactPrefab;
    /// <summary>
    /// Knockback Angle
    /// </summary>
    [field: Tooltip("넉백 Angle, 벡터 크기에 따라 넉백 증가")]
    public Vector2 KnockbackAngle;
    /// <summary>
    /// 투사체 발사 클립
    /// </summary>
    public AudioClip ProjectileShootClip;
    /// <summary>
    /// 임팩트 클립
    /// </summary>
    public AudioClip ImpactClip;

    /// <summary>
    /// 온힛효과 발동여부
    /// </summary>
    public bool isOnHit;
    public bool isShakeCam;
    /// <summary>
    /// 투사체 발사 시 ShakeCam
    /// </summary>
    public CamData camDatas;
}

namespace SOB.Weapons.Components
{
    [Serializable]
    public class WeaponProjectileActionData : ActionData
    {
        [field: SerializeField] public ProjectileData[] ProjectileActionData;
    }
}

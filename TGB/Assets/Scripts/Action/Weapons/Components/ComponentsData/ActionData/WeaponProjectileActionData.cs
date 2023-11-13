using TGB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ProjectileData
{
    /// <summary>
    /// 발사 위치
    /// </summary>
    public Vector3 Pos;
    /// <summary>
    /// 발사 각도
    /// </summary>
    public Vector3 Rot;
    /// <summary>
    /// 투사체 속도
    /// </summary>
    public float Speed;
    /// <summary>
    /// 투사체 지속시간
    /// </summary>
    [Min(0.5f)]
    public float DurationTime;
    [Tooltip("추가 데미지")]
    /// <summary>
    /// 추가 데미지
    /// </summary>
    public float AdditionalDmg;
    [Tooltip("고정 데미지(이때 데미지는 AdditionalDmg로 계산)")]
    /// <summary>
    /// 고정 데미지(이때 데미지는 AdditionalDmg로 계산)
    /// </summary>
    public bool isFixed;

    /// <summary>
    /// 발사체 피격 판정 크기
    /// </summary>
    public float Radius;
    /// <summary>
    /// 피격 판정 CircleCollider의 Offset
    /// </summary>
    public Vector2 Offset;
    /// <summary>
    /// RigidBody2D GravityScale
    /// </summary>
    public float GravityScale;
    /// <summary>
    /// 단일 피격
    /// </summary>
    public bool isSingleShoot;
    /// <summary>
    /// 넉백
    /// </summary>
    public Vector2 KnockbackAngle;
    /// <summary>
    /// Ground와 충돌 판별 여부
    /// </summary>
    public bool isCollisionGround;
    /// <summary>
    /// 이펙트 로컬 스케일
    /// </summary>
    public Vector3 EffectScale;
    /// <summary>
    /// 온힛효과 발동여부
    /// </summary>
    public bool isOnHit;
    public bool isShakeCam;
    public HomingType homingType;
    /// <summary>
    /// 투사체 발사 시 ShakeCam
    /// </summary>
    public CamData camDatas;
}

[Serializable]
public enum HomingType
{
    Done = 0,
    /// <summary>
    /// 추적 발사
    /// </summary>
    isHoming,
    /// <summary>
    /// 타겟 방향으로 직선 발사
    /// </summary>
    isToTarget,
    /// <summary>
    /// 공격 방향에 타겟이 있으면 타겟방향으로 직선 발사
    /// </summary>
    isToTarget_Direct,
}

[System.Serializable]
public struct ProjectileActionData
{
    public ProjectileData ProjectileData;
    /// <summary>
    /// 투사체
    /// </summary>
    public GameObject Projectile;
}

namespace TGB.Weapons.Components
{
    [Serializable]
    public class WeaponProjectileActionData : ActionData
    {
        [field: SerializeField] public ProjectileActionData[] ProjectileActionData;
    }
}

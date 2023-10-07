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

    public float Radius;
    public Vector2 Offset;
    public float GravityScale;
    public bool isSingleShoot;
    public Vector2 KnockbackAngle;
    /// <summary>
    /// Ground와 충돌 판별 여부
    /// </summary>
    public bool isCollisionGround;    

    /// <summary>
    /// 온힛효과 발동여부
    /// </summary>
    public bool isOnHit;
    public bool isShakeCam;
    /// <summary>
    /// 자동 추적
    /// </summary>
    public bool isHoming;
    /// <summary>
    /// 투사체 발사 시 ShakeCam
    /// </summary>
    public CamData camDatas;
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

using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "newUnitData",menuName ="Data/Unit Data")]
public class UnitData : ScriptableObject
{
    [Header("Status")]
    [Tooltip("기본 Status")]
    public StatsData statsStats;

    public string UnitName;
    public LocalizedString UnitNameLocal;
    [Tooltip("넉백지속시간")]
    public float knockBackDuration;
    [Tooltip("넉백 속도")]
    public Vector2 knockBackSpeed;

    [Header("RigidBody2D")]
    public float UnitGravity;

    [Header("InvincibleTime")]
    [Tooltip("피격 쿨타임")]
    public float invincibleTime = 1f;
    [Tooltip("터치 피격 쿨타임")]
    public float touchDamageinvincibleTime = 1f;

    [Header("Collider")]
    [Tooltip("기본 캡슐 콜라이더 Offset")]
    public Vector2 standCC2DOffset;
    [Tooltip("기본 캡슐 콜라이더 Size")]
    public Vector2 standCC2DSize;
    [Tooltip("캡슐 콜라이더 PhysicsMtrl2D")]
    public PhysicsMaterial2D UnitCC2DMaterial;

    [Header("Check Variables")]
    [Tooltip("지면 감지 거리")]
    public float groundCheckRadius = 0.1f;
    [Tooltip("벽면 감지 거리")]
    public float wallCheckDistance = 0.5f;
    [Tooltip("유닛 감지 거리")]
    public float UnitDetectedDistance = 0.5f;
    [Tooltip("지면 LayerMask")]
    public LayerMask whatIsGround;
    [Tooltip("벽면 LayerMask")]
    public LayerMask whatIsWall;
    [Tooltip("Platform LayerMask")]
    public LayerMask whatIsPlatform;
    [Tooltip("적으로 인지하는 Object LayerMask")]
    public LayerMask WhatIsEnemyUnit;

    [Header("Animator")]
    public RuntimeAnimatorController UnitAnimator;
}

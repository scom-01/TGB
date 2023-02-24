using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUnitData",menuName ="Data/Unit Data")]
public class UnitData : ScriptableObject
{
    [Header("Status")]
    [Tooltip("기본 Status")]
    public StatsData statsStats;
    [Tooltip("넉백지속시간")]
    public float knockBackDuration;
    [Tooltip("넉백 속도")]
    public Vector2 knockBackSpeed;

    [Header("InvincibleTime")]
    [Tooltip("피격 쿨타임")]
    public float invincibleTime = 1f;
    [Tooltip("터치 피격 쿨타임")]
    public float touchDamageinvincibleTime = 1f;

    [Header("Collider")]
    [Tooltip("기본 콜라이더 크기")]
    public float standColliderHeight;

    [Header("Check Variables")]
    [Tooltip("지면 감지 거리")]
    public float groundCheckRadius = 0.1f;
    [Tooltip("벽면 감지 거리")]
    public float wallCheckDistance = 0.5f;
    [Tooltip("유닛 감지 거리")]
    public float UnitDetectedDistance = 0.5f;
    [Tooltip("지면 LayerMask")]
    public LayerMask whatIsGround;
    [Tooltip("적으로 인지하는 Object LayerMask")]
    public LayerMask WhatIsEnemyUnit;
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Move State")]
    [Tooltip("움직임 Velocity")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    [Tooltip("점프 Velocity")]
    public float jumpVelocity = 15f;


    [Header("InvincibleTime")]
    [Tooltip("피격 쿨타임")]
    public float invincibleTime = 1f;

    [Header("Collider")]
    [Tooltip("기본 콜라이더 크기")]
    public float standColliderHeight;
    [Tooltip("대쉬 콜라이더 크기")]
    public float dashColliderHeight;

    [Header("In Air State")]
    [Tooltip("공중 점프 가능 시간 (점프하지않고 낙하 시 0.2s 안에 점프 가능)")]
    public float coyeteTime = 0.2f;
    [Tooltip("멀티 점프 가능 시간")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Block State")]
    public float blockTime = 1f;
    public float blockCooldown = 2f;

    [Header("Check Variables")]
    [Tooltip("바닥 체크 Radius 반지름값")]
    public float groundCheckRadius = 0.3f;
    [Tooltip("전방 벽 체크 Distance")]
    public float wallCheckDistance = 0.5f;
    [Tooltip("벽 체크 LayerMask")]
    public LayerMask whatIsGround;
    [Tooltip("Player Attack LayerMask")]
    public LayerMask playerAttackMask;
    [Tooltip("적 공격 LayerMask")]
    public LayerMask enemyAttackMask;
}

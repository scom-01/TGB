using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Status")]
    public CommonData commonStats;
    [Tooltip("넉백지속시간")]
    public float knockBackDuration;
    [Tooltip("넉백 속도")]
    public Vector2 knockBackSpeed;

    [Header("Jump State")]
    [Tooltip("점프 Velocity")]
    public float jumpVelocity = 15f;

    [Header("StateData")]    
    public float minIdleTime;
    public float maxIdleTime;


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
    [Tooltip("플레이어 감지 거리")]
    public float playerDetectedDistance = 5f;
    [Tooltip("지면 감지 거리")]
    public float groundCheckRadius = 0.1f;
    [Tooltip("벽면 감지 거리")]
    public float wallCheckDistance = 0.5f;
    [Tooltip("지면 LayerMask")]
    public LayerMask whatIsGround;
    [Tooltip("Player LayerMask")]
    public LayerMask whatIsPlayer;

    [Tooltip("Player Attack LayerMask")]
    public LayerMask playerAttackMask;
    [Tooltip("적 공격 LayerMask")]
    public LayerMask enemyAttackMask;
}

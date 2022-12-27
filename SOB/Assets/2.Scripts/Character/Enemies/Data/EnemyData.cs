using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Move State")]
    [Tooltip("������ Velocity")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    [Tooltip("���� Velocity")]
    public float jumpVelocity = 15f;

    [Header("Status")]
    [Tooltip("ü��")]
    public float maxHealth;
    [Tooltip("�˹����ӽð�")]
    public float knockBackDuration;
    [Tooltip("�˹� �ӵ�")]
    public float knockBackSpeed;

    [Header("InvincibleTime")]
    [Tooltip("�ǰ� ��Ÿ��")]
    public float invincibleTime = 1f;

    [Header("Collider")]
    [Tooltip("�⺻ �ݶ��̴� ũ��")]
    public float standColliderHeight;
    [Tooltip("�뽬 �ݶ��̴� ũ��")]
    public float dashColliderHeight;

    [Header("In Air State")]
    [Tooltip("���� ���� ���� �ð� (���������ʰ� ���� �� 0.2s �ȿ� ���� ����)")]
    public float coyeteTime = 0.2f;
    [Tooltip("��Ƽ ���� ���� �ð�")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Block State")]
    public float blockTime = 1f;
    public float blockCooldown = 2f;

    [Header("Check Variables")]
    [Tooltip("�÷��̾� ���� �Ÿ�")]
    public float playerDetectedDistance = 5f;
    [Tooltip("Player Attack LayerMask")]
    public LayerMask playerAttackMask;
    [Tooltip("�� ���� LayerMask")]
    public LayerMask enemyAttackMask;
}

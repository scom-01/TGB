using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class CommonData1 : MonoBehaviour
{
    [Header("CommonData")]
    [Tooltip("ü��")]
    public int Health;
    [Tooltip("������")]
    public float JumpVelocity;
    [Tooltip("�̵��ӵ�")]
    public float MovementSpeed;
    [Tooltip("��� ����")]
    public ElementalPower WeakElementalPower;
}*/

[CreateAssetMenu(fileName = "newPlayerData",menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    [Tooltip("������ Velocity")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    [Tooltip("���� Velocity")]
    public float jumpVelocity = 15f;
    [Tooltip("���� ���� Ƚ��")]
    public int amountOfJumps = 1;

    [Header("InvincibleTime")]
    [Tooltip("�ǰ� ��Ÿ��")]
    public float invincibleTime = 1f;
    [Tooltip("��ġ �ǰ� ��Ÿ��")]
    public float touchDamageinvincibleTime = 1f;

    [Header("Collider")]
    [Tooltip("�⺻ �ݶ��̴� ũ��")]
    public float standColliderHeight;
    [Tooltip("�뽬 �ݶ��̴� ũ��")]
    public float dashColliderHeight;

    [Header("Wall Jump State")]
    [Tooltip("�� ���� Velocity")]
    public float wallJumpVelocity = 20f;
    [Tooltip("�� ���� �ð�")]
    public float wallJumpTime = 0.4f;
    [Tooltip("�� ���� �� ���� y = 2x")]
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    [Tooltip("���� ���� ���� �ð� (���������ʰ� ���� �� 0.2s �ȿ� ���� ����)")]
    public float coyeteTime = 0.2f;
    [Tooltip("��Ƽ ���� ���� �ð�")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    [Tooltip("�� �����̵� Velocity")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    [Tooltip("�� Climb Velocity")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    [Tooltip("�� ������ �� ���� ����")]
    public Vector2 startOffset;
    [Tooltip("�� ������ �� ���� ����")]
    public Vector2 stopOffset;

    [Header("Dash State")]
    [Tooltip("�뽬 ��Ÿ��")]
    public float dashCooldown = 0.5f;
    [Tooltip("�뽬 �ʱ�ȭ ��Ÿ��")]
    public float dashResetCooldown = 0.8f;
    [Tooltip("�뽬�ð�")]
    public float dashTime = 0.2f;
    [Tooltip("�뽬 �ӵ�")]
    public float dashVelocity = 30f;
    [Tooltip("�뽬 ��밡�� Ƚ��")]
    public int dashCount = 1;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    [Tooltip("�ܻ� ���� �Ÿ�")]
    public float distBetweenAfterImages = 0.5f;

    [Header("Block State")]
    public float blockTime = 1f;
    public float blockCooldown = 2f;

    [Header("Check Variables")]
    [Tooltip("Player Attack LayerMask")]
    public LayerMask playerAttackMask;
    [Tooltip("�� ���� LayerMask")]
    public LayerMask enemyAttackMask;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : ScriptableObject
{
    [Header("Status")]
    [Tooltip("�⺻ Status")]
    public CommonData commonStats;
    [Tooltip("�˹����ӽð�")]
    public float knockBackDuration;
    [Tooltip("�˹� �ӵ�")]
    public Vector2 knockBackSpeed;

    [Header("InvincibleTime")]
    [Tooltip("�ǰ� ��Ÿ��")]
    public float invincibleTime = 1f;
    [Tooltip("��ġ �ǰ� ��Ÿ��")]
    public float touchDamageinvincibleTime = 1f;

    [Header("Collider")]
    [Tooltip("�⺻ �ݶ��̴� ũ��")]
    public float standColliderHeight;

    [Header("Check Variables")]
    [Tooltip("���� ���� �Ÿ�")]
    public float groundCheckRadius = 0.1f;
    [Tooltip("���� ���� �Ÿ�")]
    public float wallCheckDistance = 0.5f;
    [Tooltip("���� LayerMask")]
    public LayerMask whatIsGround;
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : UnitData
{
    [Header("StateData")]    
    public float minIdleTime;
    public float maxIdleTime;

    [Header("Check Variables")]
    [Tooltip("�÷��̾� ���� �Ÿ�")]
    public float playerDetectedDistance = 5f;
    [Tooltip("Player LayerMask")]
    public LayerMask whatIsPlayer;

    [Tooltip("Player Attack LayerMask")]
    public LayerMask playerAttackMask;
    [Tooltip("�� ���� LayerMask")]
    public LayerMask enemyAttackMask;
}

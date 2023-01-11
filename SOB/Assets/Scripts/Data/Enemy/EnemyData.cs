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
    [Tooltip("플레이어 감지 거리")]
    public float playerDetectedDistance = 5f;
    [Tooltip("Player LayerMask")]
    public LayerMask whatIsPlayer;

    [Tooltip("Player Attack LayerMask")]
    public LayerMask playerAttackMask;
    [Tooltip("적 공격 LayerMask")]
    public LayerMask enemyAttackMask;
}

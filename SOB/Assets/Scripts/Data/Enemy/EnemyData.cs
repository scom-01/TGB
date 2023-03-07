using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : UnitData
{
    [Header("Type")]
    public ENEMY_Form enemy_form = ENEMY_Form.Grounded;
    public ENEMY_Size enemy_size = ENEMY_Size.Small;
    public ENEMY_Level enemy_level = ENEMY_Level.Normal;

    [Header("EnemyAttack")]
    public float UnitAttackDistance;

    [Header("StateData")]
    public float minIdleTime;
    public float maxIdleTime;
}

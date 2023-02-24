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
}

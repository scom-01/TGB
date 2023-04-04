using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    [HideInInspector] public RespawnPoint[] respawnPoints;

    /// <summary>
    /// 스폰 여부
    /// </summary>
    public bool isSpawn { get; private set; }

    public int CurrentEnemyCount
    {
        get 
        { 
            return currentCount; 
        }
        set
        {
            currentCount = value;
        }
    }

    private int currentCount;
    private void Awake()
    {
        isSpawn = false;
    }
    private void Update()
    {
        if (!isSpawn)
            return;

        if (CurrentEnemyCount == 0)
        {
            this.enabled = false;
        }
    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (!isSpawn || CurrentEnemyCount != 0)
            return;

        StageManager.Inst.SPM.CurrentSpawnIndex++;
    }

    public void Spawn()
    {
        respawnPoints = GetComponentsInChildren<RespawnPoint>();

        //Test
        for (int i = 0; i < respawnPoints.Length; i++)
        {
            respawnPoints[i].SpawnEffect(respawnPoints[i].SpawnEffectPrefab, respawnPoints[i].transform.position, respawnPoints[i].transform);
            StageManager.Inst.SPM.UIEnemyCount++;
            CurrentEnemyCount++;
            //SpawnEnemy(respawnPoints[i].SpawnPrefab, respawnPoints[i].transform.position, respawnPoints[i].transform);
        }
        isSpawn = true;
    }

    public void SpawnEnemy(GameObject EnemyPrefab, Vector3 pos, Transform transform)
    {
        var enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
        if(enemy)
            StageManager.Inst.SPM.UIEnemyCount++;
    }

    public void SpawnEnemy(GameObject EnemyPrefab, Vector3 pos, Transform transform, EnemyData enemyData)
    {
        var enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
        if (enemy)
            StageManager.Inst.SPM.UIEnemyCount++;
        enemy.GetComponent<Enemy>().enemyData = enemyData;
    }

    public void SpawnEnemy(GameObject EnemyPrefab)
    {
        if (respawnPoints.Length <= 0)
        {
            Debug.LogWarning("RespawnPoints.Length < 0");
            return;
        }
        var rnd = Random.Range(0, respawnPoints.Length);
        var enemy = Instantiate(EnemyPrefab, respawnPoints[rnd].transform.position, Quaternion.identity, respawnPoints[rnd].transform);
        if (enemy)
            StageManager.Inst.SPM.UIEnemyCount++;
    }
}

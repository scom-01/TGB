using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{    
    [HideInInspector]   public RespawnPoint[] respawnPoints;

    private bool isSpawn = false;

    public int CurrentEnemyCount;

    private void Update()
    {
        if (!isSpawn)
            return;

        int curr = 0;
        for (int i = 0; i < respawnPoints.Length; i++)
        {
            curr += respawnPoints[i].GetComponentsInChildren<Enemy>().Length;
        }
        CurrentEnemyCount = curr;     
        if(CurrentEnemyCount == 0)
        {
            this.enabled = false;            
        }
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameManager.Inst.SPM.CurrentSpawnIndex++;
    }

    public void Spawn()
    {
        respawnPoints = GetComponentsInChildren<RespawnPoint>();

        //Test
        for (int i = 0; i < respawnPoints.Length; i++)
        {
            SpawnEnemy(respawnPoints[i].SpawnPrefab, respawnPoints[i].transform.position, respawnPoints[i].transform);
        }
        isSpawn = true;
    }

    public void SpawnEnemy(GameObject EnemyPrefab, Vector3 pos, Transform transform)
    {
        var enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
    }

    public void SpawnEnemy(GameObject EnemyPrefab, Vector3 pos, Transform transform, EnemyData enemyData)
    {
        var enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
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
    }
}

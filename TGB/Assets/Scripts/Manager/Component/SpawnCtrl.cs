using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    [HideInInspector] public RespawnPoint[] respawnPoints;
    /// <summary>
    /// 플레이어 감지 지역
    /// </summary>
    private BoxCollider2D DetectedArea;

    /// <summary>
    /// 스폰 여부
    /// </summary>
    public bool isSpawn { get; private set; }
    public bool isClear;

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
        isClear = false;
        DetectedArea = this.GetComponent<BoxCollider2D>();
        if (DetectedArea != null)
        {
            DetectedArea.isTrigger = true;
            DetectedArea.enabled = false;
            this.gameObject.layer = LayerMask.NameToLayer("Area");
        }
    }
    private void Update()
    {
        if (!isSpawn)
            return;

        if (CurrentEnemyCount == 0)
        {
            isClear = true;
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

        //현재 SpawnCtrl의 Enemy가 모두 처치되었을 때 다음 SpawnCtrl.Spawn()
        GameManager.Inst.StageManager.SPM.CurrentSpawnIndex++;
    }

    public void Spawn()
    {
        respawnPoints = GetComponentsInChildren<RespawnPoint>();
        DetectedArea.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null)
            return;
        //Test
        for (int i = 0; i < respawnPoints.Length; i++)
        {
            respawnPoints[i].SpawnEffect(respawnPoints[i].SpawnEffectPrefab, respawnPoints[i].transform.position, respawnPoints[i].transform);
            GameManager.Inst.StageManager.SPM.UIEnemyCount++;
            CurrentEnemyCount++;
            //SpawnEnemy(respawnPoints[i].SpawnPrefab, respawnPoints[i].transform.position, respawnPoints[i].transform);
        }
        isSpawn = true;
        if (DetectedArea != null)
        {
            DetectedArea.enabled = false;
        }
    }

    public void SpawnEnemy(GameObject EnemyPrefab, Vector3 pos, Transform transform)
    {
        var enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
        if (enemy)
            GameManager.Inst.StageManager.SPM.UIEnemyCount++;
    }

    public void SpawnEnemy(GameObject EnemyPrefab, Vector3 pos, Transform transform, EnemyData enemyData)
    {
        var enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);
        if (enemy)
            GameManager.Inst.StageManager.SPM.UIEnemyCount++;
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
            GameManager.Inst.StageManager.SPM.UIEnemyCount++;
    }
}

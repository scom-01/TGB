using SOB.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject TestEnemy;
        private RespawnPoint[] respawnPoints;

        private void OnEnable()
        {
            respawnPoints = GetComponentsInChildren<RespawnPoint>();

            //Test
            for(int i = 0; i < respawnPoints.Length; i++)
            {
                SpawnEnemy(TestEnemy);
            }
        }

        private void OnDisable()
        {
            
        }

        public void SpawnItem(GameObject SpawnPrefab, Vector3 pos,Transform transform, StatsItemSO itemData)
        {
            var item = Instantiate(SpawnPrefab, pos, Quaternion.identity, transform);
            item.GetComponent<SOB_Item>().Item = itemData;
            item.GetComponent<SOB_Item>().Init();
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
            if(respawnPoints.Length <= 0)
            {
                Debug.LogWarning("RespawnPoints.Length < 0");
                return;
            }

            var enemy = Instantiate(EnemyPrefab, respawnPoints[Random.Range(0, respawnPoints.Length)].transform.position, Quaternion.identity);            
        }
    }
}

using SOB.Item;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SOB.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        public SpawnCtrl[] SpawnCtrls;
        bool isStageClear = false;
        public int UIEnemyCount
        {
            get
            {
                if (SpawnCtrls[CurrentSpawnIndex] == null)
                    return 0;
                return SpawnCtrls[CurrentSpawnIndex].CurrentEnemyCount;
            }
        }


        public int CurrentSpawnIndex
        { 
            get
            {
                return currentSpawnIndex;
            }
            set
            {
                currentSpawnIndex = value;
                if(currentSpawnIndex < SpawnCtrls.Length)
                {
                    if (SpawnCtrls[currentSpawnIndex] != null)
                    {
                        SpawnCtrls[currentSpawnIndex].Spawn();
                    }
                }     
                else
                {
                    currentSpawnIndex = SpawnCtrls.Length - 1;
                }
            }
        }
        private int currentSpawnIndex = 0;

        private void Awake()
        {
            SpawnCtrls = this.GetComponentsInChildren<SpawnCtrl>();            
        }
        private void Start()
        {
            CurrentSpawnIndex = 0;
        }
        private void Update()
        {
            if (SpawnCtrls == null)
                return;
            for(int i = 0; i< SpawnCtrls.Length;i++)
            {
                if(!SpawnCtrls[i].isSpawn)
                {
                    return;
                }
            }
            if (isStageClear)
                return;

            Debug.Log("Stage Clear!!!");
            isStageClear = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync("Test SOB 1");
        }

        private void OnEnable()
        {
            SpawnCtrls = this.GetComponentsInChildren<SpawnCtrl>();            
        }

        private void OnDisable()
        {

        }

        public void SpawnItem(GameObject SpawnPrefab, Vector3 pos, Transform transform, StatsItemSO itemData)
        {
            var item = Instantiate(SpawnPrefab, pos, Quaternion.identity, transform);
            item.GetComponent<SOB_Item>().Item = itemData;
            item.GetComponent<SOB_Item>().Init();
        }
    }
}

using SOB.CoreSystem;
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
        private StageManager stageManager;
        public SpawnCtrl[] SpawnCtrls;
        bool isStageClear = false;
        public int UIEnemyCount
        {
            get
            {
                return _uiEnemyCount;
            }
            set
            {
                _uiEnemyCount = value;
                if (GameManager.Inst.gameObject && GameManager.Inst.StageManager)
                    GameManager.Inst.MainUI.MainPanel.EnemyPanelSystem.EnemyCountText.text = GameManager.Inst.StageManager.SPM.UIEnemyCount.ToString();
            }
        }

        private int _uiEnemyCount = 0;

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
            stageManager = this.GetComponentInParent<StageManager>();
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
            stageManager.SaveData();
            GameManager.Inst.ClearScene();
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

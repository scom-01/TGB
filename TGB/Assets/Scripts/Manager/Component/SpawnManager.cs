using TGB.Item;
using UnityEngine;

namespace TGB.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        private StageManager StageManager
        {
            get
            {
                if(stageManager == null)
                {
                    stageManager = this.GetComponentInParent<StageManager>();
                }
                return stageManager;
            }
        }
        private StageManager stageManager;
        private SpawnCtrl[] SpawnCtrls
        {
            get
            {
                if (spwanCtrls.Length == 0)
                {
                    spwanCtrls = this.GetComponentsInChildren<SpawnCtrl>();
                }
                return spwanCtrls;
            }
        }
        private SpawnCtrl[] spwanCtrls = { };
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

        /// <summary>
        /// 현재 스폰할 SpawnCtrl 인덱스
        /// </summary>
        public int CurrentSpawnIndex
        {
            get
            {
                return currentSpawnIndex;
            }
            set
            {
                currentSpawnIndex = value;
                if (currentSpawnIndex < SpawnCtrls.Length)
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

        private void Start()
        {
            CurrentSpawnIndex = 0;
            if(UIEnemyCount == 0)
            {
                if (GameManager.Inst.gameObject && GameManager.Inst.StageManager)
                    GameManager.Inst.MainUI.MainPanel.EnemyPanelSystem.EnemyCountText.text = "0";
            }
        }
        private void Update()
        {
            if (SpawnCtrls == null)
                return;

            if (StageManager.isStageClear)
            {
                return;
            }

            for (int i = 0; i < SpawnCtrls.Length; i++)
            {
                if (!SpawnCtrls[i].isSpawn || !SpawnCtrls[i].isClear)
                {
                    return;
                }
            }

            StageManager.isStageClear = true;
            StageClear();
        }

        private void StageClear()
        {
            Debug.Log("Stage Clear!!!");
            
            StageManager.OpenGate(StageManager.isStageClear);

            if (StageManager.EndingCutSceneDirector != null)
            {
                Instantiate(StageManager.EndingCutSceneDirector);
            }
        }
    }
}

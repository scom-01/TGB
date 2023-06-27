using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class EnemyCountText : MonoBehaviour
{
    [SerializeField] private ENEMY_Level Type;

    [SerializeField] private TextMeshProUGUI EnemyCountTxt;
    [SerializeField] private LocalizeStringEvent EnemyCountTxtLocalizeStringEvent;
   
    private int TypeCount
    {
        get
        {
            int enemyCount = 0;
            if(GameManager.Inst == null)
            {
                return -1;
            }
            switch (Type)
            {
                case ENEMY_Level.NormalEnemy:
                    enemyCount = GameManager.Inst.EnemyCount.Normal_Enemy_Count;
                    break;
                case ENEMY_Level.EleteEnemy:
                    enemyCount = GameManager.Inst.EnemyCount.Elete_Enemy_Count;
                    break;
                case ENEMY_Level.BossEnemy:
                    enemyCount = GameManager.Inst.EnemyCount.Boss_Enemy_Count;
                    break;
            }
            return enemyCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyCountTxtLocalizeStringEvent != null)
        {
            EnemyCountTxtLocalizeStringEvent.StringReference.SetReference("UI_Table", Type.ToString());
        }

        if (EnemyCountTxt != null)
        {
            EnemyCountTxt.text = TypeCount.ToString();
        }
    }
}

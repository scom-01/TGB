using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class EnemyCountText : MonoBehaviour
{
    private Unit Unit
    {
        get
        {
            if (unit == null)
            {
                unit = GameManager.Inst?.StageManager?.player;

                if (unit != null)
                {
                    if(DataManager.Inst !=null)
                    {
                        DataManager.Inst.JSON_DataParsing.OnChangeEnemyCount-= UpdateEnemyCountText;
                        DataManager.Inst.JSON_DataParsing.OnChangeEnemyCount+= UpdateEnemyCountText;
                    }
                    UpdateEnemyCountText();
                }
            }
            return unit;
        }
    }
    private Unit unit;

    [SerializeField] private ENEMY_Level Type;

    [SerializeField] private TextMeshProUGUI EnemyCountTxt;
    [SerializeField] private LocalizeStringEvent EnemyCountTxtLocalizeStringEvent;

    [SerializeField] private bool Showcolon;
    [SerializeField] private bool isLeftcolon;

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
                    enemyCount = DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.EnemyCount.Normal_Enemy_Count;
                    break;
                case ENEMY_Level.EleteEnemy:
                    enemyCount = DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.EnemyCount.Elete_Enemy_Count;
                    break;
                case ENEMY_Level.BossEnemy:
                    enemyCount = DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.EnemyCount.Boss_Enemy_Count;
                    break;
            }
            return enemyCount;
        }
    }

    private Canvas m_Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = this.GetComponentInParent<Canvas>();
            }
            return _canvas;
        }
    }
    private Canvas _canvas;
    // Update is called once per frame
    private void Update()
    {
        if (Unit == null)
            return;
    }
    private void UpdateEnemyCountText()
    {
        if (EnemyCountTxtLocalizeStringEvent != null)
        {
            EnemyCountTxtLocalizeStringEvent.StringReference.SetReference("UI_Table", Type.ToString());
        }

        if (EnemyCountTxt != null)
        {
            EnemyCountTxt.text = isLeftcolon ? ((Showcolon ? " : " : "") + string.Format("{0:#,##0}", TypeCount)) : (string.Format("{0:#,##0}", TypeCount) + (Showcolon ? " : " : "")); ;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    public void MoveScene()
    {
        if (GameManager.Inst == null)
            return;

        GameManager.Inst.ChangeUI(UI_State.Loading);
        GameManager.Inst.SaveData();

        if (GameManager.Inst.StageManager != null)
        {
            DataManager.Inst.NextStage(GameManager.Inst.StageManager.NextStageNumber);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }

    public void MoveTitle()
    {
        if (GameManager.Inst == null)
            return;

        GameManager.Inst.ChangeUI(UI_State.Loading);        
        GameManager.Inst.ResetData();

        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
}

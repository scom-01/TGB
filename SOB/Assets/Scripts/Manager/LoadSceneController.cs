using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    private bool isMoveScene = false;
    public void MoveScene()
    {
        if (isMoveScene)
            return;

        if (GameManager.Inst == null)
            return;
                
        GameManager.Inst.ChangeUI(UI_State.Loading);
        GameManager.Inst.SaveData();

        if (GameManager.Inst.StageManager != null)
        {
            DataManager.Inst.NextStage(GameManager.Inst.StageManager.NextStageNumber);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
        isMoveScene = true;
    }

    public void MoveTitle()
    {
        if (GameManager.Inst == null)
            return;

        GameManager.Inst.ChangeUI(UI_State.Loading);

        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }

    private void Start()
    {
        //혹시나 애니메이션이 멈춤이 발생할 경우를 대비
        Invoke("MoveScene", 5f);
    }
}

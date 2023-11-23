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

        //씬 이동할 때마다 게임 Idx 설정 //임의의 수 10
        DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs.Clear();
        for (int i = 0; i < 10; i++)
        {
            DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs.Add(UnityEngine.Random.Range(0, GlobalValue.MaxSceneIdx));
        }

        GameManager.Inst.ChangeUI(UI_State.Loading);
        
        GameManager.Inst.SaveData();
        StartCoroutine(LoadScene("LoadingScene"));
        isMoveScene = true;
    }

    public void MoveTitle()
    {
        if (GameManager.Inst == null)
            return;

        DataManager.Inst?.NextStage(2);
        StartCoroutine(LoadScene("LoadingScene"));
    }

    private void Start()
    {
        //혹시나 애니메이션이 멈춤이 발생할 경우를 대비
        Invoke("MoveScene", 5f);
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (operation.progress <= 0.9f)
        {
            yield return null;
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}

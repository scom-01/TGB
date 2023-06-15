using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    private bool isDone = false;
    private void Awake()
    {
        DataManager.Inst?.NextStage("Title");
    }

    private void Update()
    {
        if (isDone)
            return;

        if(LocalizationSettings.InitializationOperation.IsDone)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
            isDone = true;
        }
    }
}

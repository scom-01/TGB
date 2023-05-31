using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Inst?.NextStage("Title");
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
}

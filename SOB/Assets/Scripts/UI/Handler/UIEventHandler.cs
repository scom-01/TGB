using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIEventHandler : MonoBehaviour
{
    public string StageName;
    private bool isDone = false;
    public ProgressBar Loading_progressbar;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        GameManager.Inst.MainUI.MainPanel.gameObject.SetActive(false);
        GameManager.Inst.ResultUI.resultPanel.gameObject.SetActive(false);
    }

    private void OnButtonCallBack(ChangeEvent<string> evt)
    {
        Debug.Log(evt);
    }

    public void OnStartBtnClicked()
    {
        if (isDone)
            return;

        if (DataManager.Inst == null)
            return;

        DataManager.Inst.LoadScene();
        //강제로 CutScene1
        if (StageName != null && StageName != "")
            DataManager.Inst?.NextStage(StageName);
        GameManager.Inst.ClearScene();
        isDone = true;
    }
    public void OnExitBtnClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnOptionBtnClicked()
    {
        if (GameManager.Inst != null) ;
        {
            GameManager.Inst.inputHandler.ChangeCurrentActionMap("Cfg", true);
            GameManager.Inst.CfgUI.Canvas.enabled = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// button 0 = start, 1 = load, 2 = option, 3 = exit
    /// </summary>
    public List<UnityEngine.UI.Button> buttons;
    /// <summary>
    /// 버튼 중복 클릭 입력 방지
    /// </summary>
    private bool isDone = false;

    private void OnEnable()
    {
        if (DataManager.Inst != null)
        {
            DataManager.Inst.Init();
        }
        Init();
    }

    private void Init()
    {
        if (GameManager.Inst == null)
            return;

        GameManager.Inst.ChangeUI(UI_State.CutScene);

        if (DataManager.Inst.CheckJSONFile())
        {            
            buttons[1].gameObject.SetActive(true);
        }
        else
        {
            buttons[1].gameObject.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
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
        
        GameManager.Inst.ClearData();
        List<string> SceneList = new List<string>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            SceneList.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        }

        var SceneName = SceneList[3];
        SceneList = null;
        DataManager.Inst?.NextStage(SceneName.ToString());
        GameManager.Inst.ClearScene();
        isDone = true;
    }

    public void CheckSaveFile(GameObject go)
    {
        if (!DataManager.Inst.CheckJSONFile())
        {
            Debug.LogWarning("Save file not found");
            OnStartBtnClicked();
        }
        //New Game
        else
        {
            if (go != null && go.GetComponent<Canvas>() != null)
            {
                go.GetComponent<Canvas>().enabled = true;
                if (go.GetComponentsInChildren<UnityEngine.UI.Button>() != null)
                {
                    var btns = go.GetComponentsInChildren<UnityEngine.UI.Button>();
                    EventSystem.current.SetSelectedGameObject(btns[btns.Length - 1].gameObject);
                }
            }
        }
    }


    public void OnLoadBtnClicked()
    {
        if (isDone)
            return;

        if (DataManager.Inst == null)
            return;

        if (DataManager.Inst.CheckJSONFile())
        {            
            DataManager.Inst.LoadScene();
            GameManager.Inst.ClearScene();
            isDone = true;
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }    

    public void OnOptionBtnClicked()
    {
        if (GameManager.Inst != null)
        {
            GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.Cfg, true);
            GameManager.Inst.CfgUI.Canvas.enabled = true;
        }
    }
    public void OnExitBtnClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void EventSystemSetSelectedNull()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}

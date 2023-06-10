using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioClip TitleBGM;
    [SerializeField] private Transform BGM;

    public string StageName;
    public List<UnityEngine.UI.Button> buttons;
    /// <summary>
    /// 버튼 중복 클릭 입력 방지
    /// </summary>
    private bool isDone = false;
    public ProgressBar Loading_progressbar;

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
        var audio = BGM.AddComponent<AudioSource>();
        audio.clip = TitleBGM;
        DataManager.Inst?.PlayerCfgBGMLoad();
        audio.outputAudioMixerGroup = DataManager.Inst.BGM;
        audio.loop = true;
        audio.Play();
        GameManager.Inst.MainUI.Canvas.enabled = false;
        GameManager.Inst.ResultUI.Canvas.enabled = false;

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

        if (!DataManager.Inst.CheckJSONFile())
        {
            Debug.LogWarning("Save file not found");

        }
        else
        {

        }

        //강제로 CutScene1
        if (StageName != null && StageName != "")
            DataManager.Inst?.NextStage(StageName);


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

        DataManager.Inst.LoadScene();
        ////강제로 CutScene1
        //if (StageName != null && StageName != "")
        //    DataManager.Inst?.NextStage(StageName);
        GameManager.Inst.ClearScene();
        isDone = true;
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

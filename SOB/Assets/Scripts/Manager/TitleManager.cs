using AssetKits.ParticleImage.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// button 0 = start, 1 = load, 2 = option, 3 = exit
    /// </summary>
    public List<UnityEngine.UI.Button> buttons;
    private List<UnityEngine.UI.Button> btns;
    /// <summary>
    /// 버튼 중복 클릭 입력 방지
    /// </summary>
    private bool isDone = false;

    public GameObject UnlockItem_Canvas;

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

        
        //이어하기
        if (DataManager.Inst.CheckJSONFile())
        {
            buttons[1].gameObject.SetActive(true);
        }
        else
        {
            buttons[1].gameObject.SetActive(false);
            btns = buttons;
            btns.RemoveAt(1);
            for (int i = 0; i < btns.Count; i++)
            {
                Navigation nav = btns[i].navigation;

                nav.selectOnDown = btns[i + 1 < btns.Count ? i + 1 : 0];
                nav.selectOnUp = btns[i - 1 < 0 ? btns.Count - 1 : i - 1];
                btns[i].navigation = nav;
            }
        }
        
        //CheckUnlockItem
        //if(DataManager.Inst.(DataManager.Inst.JSON_DataParsing.Json_Read_DefaultData().UnlockItemIdxs))

        if (GameManager.Inst.EffectTextUI.UnlockitemNames.Count > 0)
        {
            GameManager.Inst.EffectTextUI.EffectTextOn();
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
        
        GameManager.Inst.ResetData();
        
        //새 게임 생성 시 게임 Idx 설정
        DataManager.Inst.JSON_DataParsing.SceneDataIdx = UnityEngine.Random.Range(0, 10);
        DataManager.Inst.SaveSceneDataIdx(DataManager.Inst.JSON_DataParsing.SceneDataIdx);

        DataManager.Inst?.NextStage(3);
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

        DataManager.Inst.JSON_DataParsing.SceneDataIdx = DataManager.Inst.LoadSceneDataIdx();
    }

    public void OnUnlockItemClicked()
    {
        if (UnlockItem_Canvas != null)
        {
            UnlockItem_Canvas.GetComponent<Canvas>().enabled = true;
            UnlockItem_Canvas.GetComponentInChildren<UnlockItemList>()?.SetInit();

            if(UnlockItem_Canvas.GetComponentInChildren<UnlockItemList>()?.UnlockItems.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject(UnlockItem_Canvas.GetComponentInChildren<UnlockItemList>()?.UnlockItems[0].gameObject);
            }
        }

        GameManager.Inst.EffectTextUI.EffectTextOn();
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

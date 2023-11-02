using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    private PlayableDirector Director;
    private void Awake()
    {
        Director = this.GetComponent<PlayableDirector>();
    }
    public void PlayDirector()
    {
        if (Director == null)
            return;

        Director.Play();
    }

    public void StopDirector()
    {
        if (Director == null)
            return;

        Director.Stop();
    }

    public void OnCVC(CinemachineVirtualCamera cvc)
    {
        if (cvc == null)
            return;

        cvc.enabled = true;
    }
    public void OffCVC(CinemachineVirtualCamera cvc)
    {
        if (cvc == null)
            return;

        cvc.enabled = false;
    }
        
    public void SetMainCamOrthographicSize(float size)
    {
        if (size == 0f)
            return;
        Camera.main.orthographicSize = size;
    }

    public void SetTimeScale(float scale)
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.isPause == true)
            return;

        Time.timeScale = scale;
        if (Time.timeScale == 0)
            GameManager.Inst.isPause = true;
    }

    public void ResetSceneData()
    {
        GameManager.Inst.ResetData();
    }

    #region ChangeUI
    public void ChangeUI_State(UI_State ui)
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(ui);
    }

    public void ChangeUI_GamePlay()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.GamePlay);
    }
    public void ChangeUI_Cfg()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.Cfg);
    }
    public void ChangeUI_CutScene()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.CutScene);
    }
    public void ChangeUI_Loading()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.Loading);
    }
    public void ChangeUI_Result()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.Result);
        GameManager.Inst.ResultUI?.resultPanel.UpdateResultPanel();
    }
    #endregion
}

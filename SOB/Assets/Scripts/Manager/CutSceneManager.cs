using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private string NextSceneName;

    private PlayerInputHandler inputHandler
    {
        get
        {
            if (GameManager.Inst == null)
            {
                return null;
            }
            return GameManager.Inst.inputHandler;
        }
    }
    [Range(1f, 5f)]
    [SerializeField] private float SkipDurationTime;
    private bool isDone = false;

    /// <summary>
    /// Image.Fill Origin = Top;
    /// Image.Clockwise = false;
    /// </summary>
    [SerializeField]
    private GameObject SkipObject;
    private Image FillImg
    {
        get
        {
            if (fillImg == null)
            {
                fillImg = SkipObject.GetComponentInChildren<Image>();
            }
            return fillImg;
        }
    }
    private Image fillImg = null;
    private void Update()
    {
        //DashInputStop으로 현재 
        if (!inputHandler.InteractionInputStop && !isDone)
        {
            if (FillImg != null)
            {
                SkipObject.SetActive(true);
                FillImg.fillAmount = (inputHandler.interactionInputStartTime + SkipDurationTime - Time.time) / SkipDurationTime;
            }
            if (Time.time >= inputHandler.interactionInputStartTime + SkipDurationTime)
            {
                OnTriggerSceneEnd();
                isDone = true;
            }
        }
        else
        {
            if (FillImg != null)
            {
                SkipObject.SetActive(false);
                FillImg.fillAmount = 1f;
            }
        }
    }
    public void PlayDirector(PlayableDirector playableDirector)
    {
        if (playableDirector == null)
        { return; }
        playableDirector.Play();
    }


    public void OnTriggerSceneEnd()
    {
        DataManager.Inst.NextStage(NextSceneName);
        GameManager.Inst.ClearScene();
    }
}

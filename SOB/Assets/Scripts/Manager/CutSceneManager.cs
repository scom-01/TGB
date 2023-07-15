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
    public string CurrStageName
    {
        get
        {
            return SceneManager.GetActiveScene().name;
        }
    }
    public int CurrStageNumber
    {
        get
        {
            for (int i = 0; i < GameManager.Inst.SceneNameList.Count; i++)
            {
                if (GameManager.Inst.SceneNameList[i] == GameManager.Inst.StageManager.CurrStageName)
                {
                    DataManager.Inst.JSON_DataParsing.Json_Overwrite_SceneName(i);
                    return i;
                }
            }
            return 0;
        }
    }
    public int NextStageNumber;

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

    private void Start()
    {
        if (PlayableDirector != null)
        {
            PlayableDirector.stopped += OnTriggerSceneEnd;
            if(this.GetComponent<SkipCutSceneIndex>() != null)
            {
                this.GetComponent<SkipCutSceneIndex>().CheckSkipCutScene();
            }
        }
    }
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
                if (this.GetComponent<SkipCutSceneIndex>() != null) 
                {
                    this.GetComponent<SkipCutSceneIndex>().AddSkipCutScene();
                }
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
        DataManager.Inst.NextStage(NextStageNumber);
        GameManager.Inst.ClearScene();
    }

    public void OnTriggerSceneEnd(PlayableDirector playableDirector)
    {
        DataManager.Inst.NextStage(NextStageNumber);
        GameManager.Inst.ClearScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipCutSceneIndex : MonoBehaviour
{
    [SerializeField] private int idx;
    [SerializeField] private PlayableDirector Director;

    //게임 중간 보스 및 중간보스 혹은 대화를 통한 컷씬이 아닌 경우
    //CutScene씬에 지정된 CutScene을 SkipCutSceneList에 추가 하기 위함이므로 바로 SaveSkipCutSceneList
    public void AddSkipCutScene()
    {
        if (DataManager.Inst == null)
        {
            return;
        }

        if (this.GetComponent<CutSceneManager>() != null)
        {
            idx = this.GetComponent<CutSceneManager>().CurrStageNumber;
            if(idx == 0)
            {
                Debug.LogWarning("Scene Not found");
            }

            if (!DataManager.Inst.JSON_DataParsing.SkipCutSceneList.Contains(idx))
            {
                DataManager.Inst.JSON_DataParsing.SkipCutSceneList.Add(idx);
                DataManager.Inst.SaveSkipCutSceneList(DataManager.Inst.JSON_DataParsing.SkipCutSceneList);
            }
        }
        else
        {
            if (!DataManager.Inst.JSON_DataParsing.SkipBossCutScene.Contains(idx))
            {
                DataManager.Inst.JSON_DataParsing.SkipBossCutScene.Add(idx);
                DataManager.Inst.SaveSkipBossCutSceneList(DataManager.Inst.JSON_DataParsing.SkipBossCutScene);
            }
        }

        
    }
    public void AddSkipCutScene(int _idx)
    {
        if (DataManager.Inst == null)
        {
            return;
        }

        if (!DataManager.Inst.JSON_DataParsing.SkipCutSceneList.Contains(_idx))
        {
            DataManager.Inst.JSON_DataParsing.SkipCutSceneList.Add(_idx);
        }
    }

    public void CheckSkipCutScene()
    {
        if(DataManager.Inst == null)
        {
            return;
        }

        if(DataManager.Inst.JSON_DataParsing.SkipCutSceneList.Contains(idx))
        {
            if (Director != null)
            {
                Director.Stop();
            }
            return;
        }
    }
    public void CheckSkipCutScene(int _idx)
    {
        if(DataManager.Inst == null)
        {
            if(Director!=null)
            {
                Director.Stop();
                Director.Evaluate();
            }
            return;
        }

        if(!DataManager.Inst.JSON_DataParsing.SkipCutSceneList.Contains(_idx))
        {
            return;
        }
    }
}

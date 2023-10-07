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

            if (!DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.Contains(idx))
            {
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.Add(idx);
            }
        }
        else
        {
            if (!DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipBossCutScene.Contains(idx))
            {
                DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipBossCutScene.Add(idx);
            }
        }

        
    }
    public void AddSkipCutScene(int _idx)
    {
        if (DataManager.Inst == null)
        {
            return;
        }

        if (!DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.Contains(_idx))
        {
            DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.Add(_idx);
        }
    }

    public void CheckSkipCutScene()
    {
        if(DataManager.Inst == null)
        {
            return;
        }

        if(DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.Contains(idx))
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

        if(!DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.Contains(_idx))
        {
            return;
        }
    }
}

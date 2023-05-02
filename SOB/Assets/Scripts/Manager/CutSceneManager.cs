using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private string SceneName;

    public void PlayDirector(PlayableDirector playableDirector)
    {
        if (playableDirector == null)
        { return; }
        playableDirector.Play();
    }

    public void OnTriggerSceneEnd()
    {
        DataManager.Inst.NextStage(SceneName);
        GameManager.Inst.ClearScene();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimeLineController : MonoBehaviour
{
    public PlayableDirector PlayableDirector;
    public TimelineAsset timeline;
    public void OffMainCam()
    {
        GameManager.Inst.StageManager.Cam.gameObject.SetActive(false);
    }

    public void Play()
    {
        PlayableDirector.Play();
    }
    public void Play(PlayableDirector _playableDirector)
    {
        _playableDirector.Play();
    }

    public void PlayFromTimeline()
    {
        PlayableDirector.Play(timeline);
    }

    public void DestroyDirector()
    {
        GameManager.Inst.StageManager.Cam.gameObject.SetActive(true);
        Destroy(PlayableDirector.gameObject);
    }
    public void DestroyDirector(PlayableDirector _playableDirector)
    {
        GameManager.Inst.StageManager.Cam.gameObject.SetActive(true);
        Destroy(_playableDirector.gameObject);
    }
    public void Stop()
    {
        PlayableDirector.Stop();
    }
    public void Stop(PlayableDirector _playableDirector)
    {
        _playableDirector.Stop();
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
}
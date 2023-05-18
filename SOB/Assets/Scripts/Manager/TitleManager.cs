using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioClip TitleBGM;
    [SerializeField] private Transform BGM;

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
    }
}

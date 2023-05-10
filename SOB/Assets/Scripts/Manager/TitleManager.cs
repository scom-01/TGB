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
    }
    // Start is called before the first frame update
    void Start()
    {
        var audio =  BGM.AddComponent<AudioSource>();
        audio.clip = TitleBGM;
        DataManager.Inst.PlayerCfgBGMLoad();
        audio.outputAudioMixerGroup = DataManager.Inst.BGM;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

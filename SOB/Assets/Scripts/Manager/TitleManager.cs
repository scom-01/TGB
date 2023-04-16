using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioClip TitleBGM;
    [SerializeField] private Transform BGM;
    // Start is called before the first frame update
    void Start()
    {
        var audio =  BGM.AddComponent<AudioSource>();
        audio.clip = TitleBGM;
        DataManager.Inst.PlayerCfgBGMLoad();
        audio.volume = DataManager.Inst.BGM_Volume;
        Debug.Log($"TitleManager audioVolume{audio.volume}");
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

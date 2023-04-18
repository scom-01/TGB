using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;

    public void OnTriggerSoundTrackStart(AudioClip audioClip)
    {
        var sounds = GameObject.FindGameObjectsWithTag(GlobalValue.SoundContainerTagName);
        if (sounds.Length > 0)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].GetComponent<SoundSync>() == null)
                    continue;

                if (sounds[i].GetComponent<SoundSync>().isBGM)
                {
                    var audioSource = sounds[i].AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.playOnAwake = true;
                    audioSource.loop = false;
                    if (DataManager.Inst != null)
                    {
                        audioSource.volume = DataManager.Inst.BGM_Volume;
                    }
                    else
                    {
                        audioSource.volume = 1;
                    }
                    audioSource.Play();
                    Destroy(audioSource, audioSource.clip.length);
                    break;
                }
                else
                {
                    var audioSource = sounds[i].AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.playOnAwake = true;
                    audioSource.loop = false;
                    if (DataManager.Inst != null)
                    {
                        audioSource.volume = DataManager.Inst.SFX_Volume;
                    }
                    else
                    {
                        audioSource.volume = 1;
                    }
                    audioSource.Play();
                    Destroy(audioSource, audioSource.clip.length);
                    break;
                }
            }
        }
        Debug.Log("Test CutScene");
    }
}

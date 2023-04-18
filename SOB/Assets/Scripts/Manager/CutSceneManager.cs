using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;

    public void OnTriggerSoundTrackStart(AudioClip audioClip)
    {
        var sounds = GameObject.FindGameObjectsWithTag(GlobalValue.SoundContainerTagName);
        if (sounds.Length == 0)
        {
            return;
        }
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
        Debug.Log("Test CutScene");
    }
    public void OnTriggerSoundTrackEnd(AudioClip audioClip)
    {
        var sounds = GameObject.FindGameObjectsWithTag(GlobalValue.SoundContainerTagName);
        if (sounds.Length == 0)
        {
            return;
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].GetComponent<SoundSync>() == null)
                continue;

            if (sounds[i].GetComponent<SoundSync>().isBGM)
            {
                var sources = sounds[i].GetComponents<AudioSource>();
                for(int j = 0; j < sources.Length; j++)
                {
                    if (sources[j].clip == audioClip)
                    {
                        Destroy(sources[j]);
                    }
                }
                break;
            }
            else
            {
                var sources = sounds[i].GetComponents<AudioSource>();
                for (int j = 0; j < sources.Length; j++)
                {
                    if (sources[j].clip == audioClip)
                    {
                        Destroy(sources[j]);
                    }
                }
                break;
            }
        }
    }

    IEnumerator FadeOutSounds(AudioSource audioSource, float duration)
    {
        Destroy(audioSource,duration);
        while (true)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0, duration);
            if (audioSource.volume <= 0)
            {
                yield return null;                
            }           
        }
    }
}

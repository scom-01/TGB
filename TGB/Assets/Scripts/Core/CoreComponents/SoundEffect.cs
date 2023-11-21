using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace TGB.CoreSystem
{    
    public class SoundEffect : CoreComponent
    {
        private Transform SFX_soundContainer;
        protected override void Awake()
        {
            base.Awake();
            if (GameObject.FindGameObjectWithTag(GlobalValue.SFX_SoundContainerTagName).transform != null)
            {
                SFX_soundContainer = GameObject.FindGameObjectWithTag(GlobalValue.SFX_SoundContainerTagName).transform;
            }
        }
        public void AudioSpawn(AudioPrefab audioData)
        {
            if (audioData.Clip == null)
            {
                Debug.LogWarning("Clip is Null");
                return;
            }

            var soundlist = SFX_soundContainer.GetComponents<AudioSource>();
            for (int i = 0; i < SFX_soundContainer.GetComponents<AudioSource>().Length; i++)
            {
                if (soundlist[i].clip == audioData.Clip)
                {
                    Destroy(soundlist[i]);
                }
            }
            var audioSource = SFX_soundContainer.AddComponent<AudioSource>();
            audioSource.clip = audioData.Clip;
            audioSource.volume = audioData.Volume;
            audioSource.playOnAwake = true;
            audioSource.outputAudioMixerGroup = DataManager.Inst.SFX;
            audioSource.loop = false;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
        }
        public void AudioSpawn(AudioClip audioClip, float volume = 1f)
        {
            if (audioClip == null)
            {
                Debug.LogWarning("Clip is Null");
                return;
            }

            var soundlist = SFX_soundContainer.GetComponents<AudioSource>();
            for (int i = 0; i < SFX_soundContainer.GetComponents<AudioSource>().Length; i++)
            {
                if (soundlist[i].clip == audioClip)
                {
                    Destroy(soundlist[i]);
                }
            }
            var audioSource = SFX_soundContainer.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.playOnAwake = true;
            audioSource.outputAudioMixerGroup = DataManager.Inst.SFX;
            audioSource.loop = false;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
        }
    }
}

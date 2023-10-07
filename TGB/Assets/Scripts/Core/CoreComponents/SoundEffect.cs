using Unity.VisualScripting;
using UnityEngine;

namespace TGB.CoreSystem
{
    public class SoundEffect : CoreComponent
    {
        private Transform soundContainer;
        protected override void Awake()
        {
            base.Awake();
            if (GameObject.FindGameObjectWithTag(GlobalValue.SoundContainerTagName).transform != null)
            {
                soundContainer = GameObject.FindGameObjectWithTag(GlobalValue.SoundContainerTagName).transform;
            }
        }

        public void AudioSpawn(AudioClip audioClip)
        {
            if (audioClip == null)
            {
                Debug.LogWarning("Clip is Null");
                return;
            }

            var soundlist = soundContainer.GetComponents<AudioSource>();
            for (int i = 0; i < soundContainer.GetComponents<AudioSource>().Length; i++)
            {
                if(soundlist[i].clip == audioClip)
                {
                    Destroy(soundlist[i]);                    
                }
            }
            var audioSource = soundContainer.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.playOnAwake = true;
            audioSource.outputAudioMixerGroup = DataManager.Inst.SFX;
            audioSource.loop = false;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
        }
    }
}

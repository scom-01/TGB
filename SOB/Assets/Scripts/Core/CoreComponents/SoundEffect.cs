using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class SoundEffect : CoreComponent
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void AudioSpawn(AudioClip audioClip)
        {
            if (audioClip == null)
            {
                Debug.LogWarning("Clip is Null");
                return;
            }

            var audioSource = this.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.playOnAwake = true;
            audioSource.loop = false;
            audioSource.volume = 1.0f;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
        }
    }
}

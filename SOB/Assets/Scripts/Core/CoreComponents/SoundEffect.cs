using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class SoundEffect : CoreComponent
    {
        [TagField]
        [field: SerializeField] private string soundContainerTagName;
        private Transform soundContainer;
        protected override void Awake()
        {
            base.Awake();
            if (GameObject.FindGameObjectWithTag(soundContainerTagName).transform != null)
            {
                soundContainer = GameObject.FindGameObjectWithTag(soundContainerTagName).transform;
            }
        }

        public void AudioSpawn(AudioClip audioClip)
        {
            if (audioClip == null)
            {
                Debug.LogWarning("Clip is Null");
                return;
            }

            var audioSource = soundContainer.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.playOnAwake = true;
            audioSource.loop = false;
            audioSource.volume = DataManager.Inst.SFX_Volume;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
        }
    }
}

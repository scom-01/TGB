using Cinemachine;
using Unity.VisualScripting;
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

            var soundlist = soundContainer.GetComponents<AudioSource>();
            for (int i = 0; i < soundContainer.GetComponents<AudioSource>().Length; i++)
            {
                if(soundlist[i].clip == audioClip)
                {
                    return;
                }
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

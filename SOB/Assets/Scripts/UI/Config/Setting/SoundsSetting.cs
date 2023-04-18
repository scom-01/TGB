using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SOB.CfgSetting
{
    public class SoundsSetting : MonoBehaviour
    {
        Slider slider;
        float volume;
        [SerializeField] private bool isBGM;
        [SerializeField] private AudioMixerGroup mixerGroup;
        private void Awake()
        {
            slider = GetComponent<Slider>();
            SoundsSettingInit();
            volume = slider.value;
        }

        private void OnEnable()
        {
            SoundsSettingInit();
        }

        public void SoundsSettingInit()
        {
            if (isBGM)
            {
                DataManager.Inst.PlayerCfgBGMLoad();
                slider.value = DataManager.Inst.BGM_Volume;
            }
            else
            {
                DataManager.Inst.PlayerCfgSFXLoad();
                slider.value = DataManager.Inst.SFX_Volume;
            }
            OnChangeVolume();
        }
        public void OnChangeVolume()
        {
            if (slider == null)
                slider = GetComponent<Slider>();

            volume = slider.value;
            var sounds = GameObject.FindGameObjectsWithTag(GlobalValue.SoundContainerTagName);
            if (isBGM)
            {                
                foreach(var sound in sounds)
                {
                    if(sound.GetComponent<SoundSync>().isBGM)
                    {
                        var sources = sound.GetComponents<AudioSource>();
                        foreach (var source in sources)
                        {
                            source.outputAudioMixerGroup = mixerGroup;
                            source.volume = volume;
                            mixerGroup.audioMixer.SetFloat("BGM_Volume", Mathf.Log10(volume) * 20);
                        }
                    }                    
                }
                DataManager.Inst.PlayerCfgBGMSave(volume);
            }
            else
            {
                foreach (var sound in sounds)
                {
                    if (!sound.GetComponent<SoundSync>().isBGM)
                    {
                        var sources = sound.GetComponents<AudioSource>();
                        foreach (var source in sources)
                        {
                            source.outputAudioMixerGroup = mixerGroup;
                            source.volume = volume;
                            mixerGroup.audioMixer.SetFloat("SFX_Volume", Mathf.Log10(volume) * 20);
                        }
                    }
                }
                DataManager.Inst.PlayerCfgSFXSave(volume);
            }
        }
    }
}
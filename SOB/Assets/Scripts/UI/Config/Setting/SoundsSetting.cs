using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SOB.CfgSetting
{
    public class SoundsSetting : MonoBehaviour
    {
        Slider slider;
        float volume;
        [SerializeField] private bool isBGM;

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
                            source.volume = volume;
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
                            source.volume = volume;
                        }
                    }
                }
                DataManager.Inst.PlayerCfgSFXSave(volume);
            }
        }
    }
}
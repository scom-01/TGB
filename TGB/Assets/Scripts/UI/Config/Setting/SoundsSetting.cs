using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TGB.CfgSetting
{
    public class SoundsSetting : MonoBehaviour
    {
        Slider slider;
        float volume;
        [SerializeField] TextMeshProUGUI GaugeText;
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
            if(mixerGroup == DataManager.Inst.BGM)
            {
                DataManager.Inst.PlayerCfgBGMLoad();
                slider.value = DataManager.Inst.BGM_Volume;
                SetBGMVolume();
            }
            else if(mixerGroup == DataManager.Inst.SFX)
            {
                DataManager.Inst.PlayerCfgSFXLoad();
                slider.value = DataManager.Inst.SFX_Volume;
                SetSFXVolume();
            }
        }
        public void SetBGMVolume()
        {
            if (slider == null)
                slider = GetComponent<Slider>();

            volume = slider.value;
            Debug.Log("BGM vol =" + volume);
            if (DataManager.Inst != null)
            {
                DataManager.Inst.BGM.audioMixer.SetFloat(GlobalValue.BGM_Vol, Mathf.Log10(volume) * 20);
                DataManager.Inst.PlayerCfgBGMSave(volume);
            }
            else
                mixerGroup.audioMixer.SetFloat(GlobalValue.BGM_Vol, Mathf.Log10(volume) * 20);

            if (GaugeText != null)
                GaugeText.text = ((int)(volume * 100f)).ToString();
        }

        public void SetSFXVolume()
        {
            if (slider == null)
                slider = GetComponent<Slider>();

            volume = slider.value;

            if (DataManager.Inst != null)
            { 
                DataManager.Inst.SFX.audioMixer.SetFloat(GlobalValue.SFX_Vol, Mathf.Log10(volume) * 20);
                DataManager.Inst.PlayerCfgSFXSave(volume);
            }
            else
                mixerGroup.audioMixer.SetFloat(GlobalValue.SFX_Vol, Mathf.Log10(volume) * 20);

            if (GaugeText != null)
                GaugeText.text = ((int)(volume * 100f)).ToString();
        }
    }
}
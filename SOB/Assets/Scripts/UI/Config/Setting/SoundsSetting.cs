using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsSetting : MonoBehaviour
{
    Slider slider;
    float volume;
    [SerializeField] private bool isBGM;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        volume = slider.value;
    }

    private void OnEnable()
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
    }
    public void OnChangeVolume()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        volume = slider.value;
        if (isBGM)
        {
            DataManager.Inst.PlayerCfgBGMSave(volume);
        }
        else
        {
            DataManager.Inst.PlayerCfgSFXSave(volume);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace SCOM.CfgSetting
{
    public class LanguageSetting : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelTxt;
        private void OnEnable()
        {
            DataManager.Inst?.PlayerCfgLanguageLoad();
        }

        public void OnClickLeft()
        {
            if (DataManager.Inst.localizationSettings.GetSelectedLocale() == DataManager.Inst.localizationSettings.GetAvailableLocales().Locales[0])
            {
                DataManager.Inst.localizationIdx = DataManager.Inst.localizationSettings.GetAvailableLocales().Locales.Count - 1;
            }
            else
            {
                DataManager.Inst.localizationIdx--;
            }
            StartCoroutine(ChangeRoutine(DataManager.Inst.localizationIdx));
            DataManager.Inst?.PlayerCfgLanguageSave();
        }
        public void OnClickRight()
        {
            if (DataManager.Inst.localizationSettings.GetSelectedLocale() == DataManager.Inst.localizationSettings.GetAvailableLocales().Locales[
                DataManager.Inst.localizationSettings.GetAvailableLocales().Locales.Count-1])
            {
                DataManager.Inst.localizationIdx = 0;
            }
            else
            {
                DataManager.Inst.localizationIdx++;
            }
            StartCoroutine(ChangeRoutine(DataManager.Inst.localizationIdx));
            DataManager.Inst?.PlayerCfgLanguageSave();
        }

        IEnumerator ChangeRoutine(int index)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}

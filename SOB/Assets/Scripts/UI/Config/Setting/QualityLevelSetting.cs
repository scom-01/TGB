using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace SOB.CfgSetting
{
    public class QualityLevelSetting : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelTxt;

        private void OnEnable()
        {
            DataManager.Inst?.PlayerCfgQualityLoad();
            ChangeText();
        }

        private void Start()
        {
            LocalizationSettings.SelectedLocaleChanged += ChangeText;
        }

        private void OnDestroy()
        {            
            LocalizationSettings.SelectedLocaleChanged -= ChangeText;
        }

        public void OnClickLeft()
        {
            if (QualitySettings.GetQualityLevel() == 0)
            {
                QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);
            }
            else
            {
                QualitySettings.DecreaseLevel();
            }
            ChangeText();
            DataManager.Inst?.PlayerCfgQualitySave();
        }
        public void OnClickRight()
        {
            if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 1)
            {
                QualitySettings.SetQualityLevel(0);
            }
            else
            {
                QualitySettings.IncreaseLevel();
            }
            ChangeText();
            DataManager.Inst?.PlayerCfgQualitySave();
        }

        private void ChangeText()
        {
            Locale currentLocale = LocalizationSettings.SelectedLocale;
            var str = Resources.Load(GlobalValue.UI_Table).name;
            //Quality의 설정값과 동일한 Key값을 미리 UI Table안에 설정해놓음 ex)LOW : 낮음            
            LevelTxt.text = LocalizationSettings.StringDatabase.GetLocalizedString(str, (QualitySettings.names[QualitySettings.GetQualityLevel()]).ToString().ToUpper(), currentLocale);
        }
        private void ChangeText(Locale locale)
        {
            Locale currentLocale = LocalizationSettings.SelectedLocale;
            var str = Resources.Load(GlobalValue.UI_Table).name;
            //Quality의 설정값과 동일한 Key값을 미리 UI Table안에 설정해놓음 ex)LOW : 낮음);
            LevelTxt.text = LocalizationSettings.StringDatabase.GetLocalizedString(str, (QualitySettings.names[QualitySettings.GetQualityLevel()]).ToString().ToUpper(), currentLocale);            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

namespace TGB.CfgSetting
{
    public class QualityLevelSetting : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelTxt;
        private void OnEnable()
        {
            DataManager.Inst?.PlayerCfgQualityLoad();            
        }

        /// <summary>
        /// Quality Left Click
        /// </summary>
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
        /// <summary>
        /// Quality Right Click
        /// </summary>
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

        public void ChangeText()
        {
            //Quality의 설정값과 동일한 Key값을 미리 UI Table안에 설정해놓음 ex)LOW : 낮음
            //var str = Resources.Load(GlobalValue.UI_Table).name;
            LevelTxt.text = (QualitySettings.names[QualitySettings.GetQualityLevel()]).ToString().ToUpper();
            if (DataManager.Inst != null)
            {
                var table = DataManager.Inst?.localizationSettings.GetStringDatabase().DefaultTable ?? "UI_Table";
                LevelTxt.GetComponent<LocalizeStringEvent>()?.StringReference.SetReference(table, LevelTxt.text);
            }   
            else
            {
                LevelTxt.GetComponent<LocalizeStringEvent>()?.StringReference.SetReference("UI_Table", LevelTxt.text);
            }
        }
    }
}

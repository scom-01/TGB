using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SOB.CfgSetting
{
    public class QualityLevelSetting : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelTxt;

        private void OnEnable()
        {
            DataManager.Inst?.PlayerCfgQualityLoad();
            LevelTxt.text = (QualitySettings.names[QualitySettings.GetQualityLevel()]).ToString().ToUpper();
        }

        public void OnClickLeft()
        {
            if (QualitySettings.GetQualityLevel() == 0)
            {
                QualitySettings.SetQualityLevel(QualitySettings.names.Length);
            }
            else
            {
                QualitySettings.DecreaseLevel();
            }

            LevelTxt.text = (QualitySettings.names[QualitySettings.GetQualityLevel()]).ToString().ToUpper();
            DataManager.Inst?.PlayerCfgQualitySave();
        }
        public void OnClickRight()
        {
            if(QualitySettings.GetQualityLevel() == QualitySettings.names.Length)
            {
                QualitySettings.SetQualityLevel(0);
            }
            else
            {
                QualitySettings.IncreaseLevel();
            }
            LevelTxt.text = (QualitySettings.names[QualitySettings.GetQualityLevel()]).ToString().ToUpper();
            DataManager.Inst?.PlayerCfgQualitySave();
        }
    }
}

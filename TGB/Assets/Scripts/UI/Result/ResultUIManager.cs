using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TGB.Manager
{
    public class ResultUIManager : UIManager
    {
        public ResultPanelUI resultPanel
        {
            get
            {
                if (_resultPanel == null)
                {
                    _resultPanel = this.GetComponentInChildren<ResultPanelUI>();
                }
                return _resultPanel;
            }
        }
        private ResultPanelUI _resultPanel;

        public Button GoTitleBtn
        {
            get => GetComponentInChildren<Button>();
        }
    }
}

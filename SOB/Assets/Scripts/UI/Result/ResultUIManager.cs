using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SOB.Manager
{
    public class ResultUIManager : MonoBehaviour
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
        public Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponent<Canvas>();
                }
                return canvas;
            }
        }
        private Canvas canvas;

        public Button GoTitleBtn
        {
            get => GetComponentInChildren<Button>();
        }
    }
}

using UnityEngine.UI;

namespace SCOM.Manager
{
    public class ResultUIManager : UI.UIManager
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

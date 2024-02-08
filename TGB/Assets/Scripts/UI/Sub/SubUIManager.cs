using UnityEngine;

namespace SCOM.Manager
{
    public class SubUIManager : UI.UIManager
    {        
        public UI.ItemDetailUI DetailSubUI;
        public InventoryUI InventorySubUI;

        public void isRight(bool isright)
        {
            if(isright)
            {
                DetailSubUI.GetComponent<RectTransform>().position = new Vector2(Screen.width * 3/ 4, Screen.height / 2);
            }
            else
            {
                DetailSubUI.GetComponent<RectTransform>().position = new Vector2(Screen.width / 4, Screen.height / 2);
            }
        }

        public void SetSubUI(bool OnOff)
        {
            if(DetailSubUI != null)
            {
                DetailSubUI.Canvas.enabled = OnOff;
                var KeySettings = this.gameObject.GetComponentsInChildren<KeySetting>();
                foreach (var keySetting in KeySettings)
                {
                    keySetting.UpdateDisplayText();
                }
            }
        }
    }
}

using UnityEngine;

namespace SCOM.Manager
{
    public class ReforgingUIManager : UI.UIManager
    {
        public Canvas[] ChildrensCanvas
        {
            get
            {
                if (childrensCanvas == null)
                {
                    childrensCanvas = GetComponentsInChildren<Canvas>();
                }
                return childrensCanvas;
            }
        }
        private Canvas[] childrensCanvas;

        public EquipWeapon equipWeapon;

        public void EnabledChildrensCanvas(bool enabled)
        {
            for (int i = 0; i < ChildrensCanvas.Length; i++)
            {                
                ChildrensCanvas[i].enabled = enabled;
            }
        }
    }
}

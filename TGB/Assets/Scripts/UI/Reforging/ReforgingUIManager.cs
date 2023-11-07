using TGB.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Manager
{
    public class ReforgingUIManager : UIManager
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

using SOB.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class ReforgingUIManager : MonoBehaviour
    {
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

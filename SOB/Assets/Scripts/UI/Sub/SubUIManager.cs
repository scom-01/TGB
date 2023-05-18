using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SOB.Manager
{
    public class SubUIManager : MonoBehaviour
    {        
        public DetailUI DetailSubUI;
        public InventoryUI InventorySubUI;
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
        private void Awake()
        {
            //DetailSubUI = this.GetComponentInChildren<DetailUI>();
            //InventorySubUI = this.GetComponentInChildren<InventoryUI>();
        }

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
            }
        }
    }
}

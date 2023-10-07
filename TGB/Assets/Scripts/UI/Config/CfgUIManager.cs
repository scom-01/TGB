using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Manager
{
    public class CfgUIManager : MonoBehaviour
    {
        public ConfigPanelUI ConfigPanelUI;
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
    }
}

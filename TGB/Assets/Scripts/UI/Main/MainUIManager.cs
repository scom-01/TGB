using TGB.CoreSystem;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TGB.Manager
{
    public class MainUIManager : MonoBehaviour
    {
        public MainPanelUI MainPanel;
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
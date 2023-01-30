using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class SubUIManager : MonoBehaviour
    {
        
        public GameObject DetailSubUI;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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
                DetailSubUI.SetActive(OnOff);                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class ResultUIManager : MonoBehaviour
    {
        public ResultPanelUI resultPanel;
        // Start is called before the first frame update
        void Start()
        {
            if(resultPanel == null)
            {
                resultPanel = this.GetComponentInChildren<ResultPanelUI>();
            }
        }
    }
}

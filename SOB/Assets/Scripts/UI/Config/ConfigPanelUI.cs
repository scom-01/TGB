using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class ConfigPanelUI : MonoBehaviour
    {
        public CfgBtn[] cfgBtns;
        private void Awake()
        {
            cfgBtns = this.GetComponentsInChildren<CfgBtn>();
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

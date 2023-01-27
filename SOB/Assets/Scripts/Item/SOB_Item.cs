using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Item
{
    public class SOB_Item : MonoBehaviour
    {
        public Core ItemCore { get; private set; }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void InitializeItem(Core core)
        {
            this.ItemCore = core;
        }
    }
}

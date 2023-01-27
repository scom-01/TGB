using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Item
{
    public class SOB_Item : MonoBehaviour
    {
        [field: SerializeField] public ItemDataSO itemData { get; private set; }
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

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if(coll.gameObject.layer == LayerMask.GetMask("Player"))
            {
                if (itemData != null ? true : false)
                {
                    
                }
            }
        }


    }
}

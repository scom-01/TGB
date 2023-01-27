using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SOB.Item
{
    public class SOB_Item : MonoBehaviour
    {
        [field: SerializeField] public ItemDataSO itemData { get; private set; }
        public Core ItemCore { get; private set; }

        private Transform particleContainer;
        private void Awake()
        {
            particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
        }
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
            if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (itemData != null ? true : false)
                {
                    Instantiate(itemData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity, particleContainer);
                    Debug.LogWarning($"Get {this.name}");
                    Destroy(this.gameObject);
                }
                else
                {
                    Debug.LogWarning("itemData is null");
                }

            }
        }
    }
}

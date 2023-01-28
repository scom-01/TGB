using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace SOB.Item
{
    public class SOB_Item : MonoBehaviour
    {
        [field: SerializeField] public ItemDataSO itemData { get; private set; }
        public Core ItemCore { get; private set; }

        [SerializeField]
        private float DetectedRadius;

        private CircleCollider2D CC2D;
        private Transform particleContainer;
        private void Awake()
        {
            particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
        }
        private void OnEnable()
        {
            CC2D = GetComponent<CircleCollider2D>();
            if(CC2D != null)
            {
                CC2D.isTrigger = true;
                CC2D.radius = DetectedRadius;
            }
        }

        private void OnDisable()
        {

        }

        private void InitializeItem(Core core)
        {
            this.ItemCore = core;
        }
        public void Detected()
        {

        }
        public void UnDetected()
        {

        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (!itemData.ConflictUse)
                return;

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

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
        public Unit unit;

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
            CC2D = GetComponentInChildren<CircleCollider2D>();
            if(CC2D != null)
            {
                CC2D.isTrigger = true;
                CC2D.radius = DetectedRadius;
            }

            if(itemData.Event_item_type.Count > 0)
            {
                for(int i = 0; i< itemData.coroutineName.Length; i++)
                {
                    itemData.Event_item_type.Add(itemData.coroutineName[i], itemData.coroutineValue[i]);
                }
            }
        }

        

        private void OnDisable()
        {
            //아이템이 가진 효과 List StartCoroutine
            for(int i = 0; i < itemData.coroutineName.Length; i++)
            {
                //TODO: Event 내용 IEnumerator 로 코루틴화
                //StartCoroutine(itemData.coroutineName[i], itemData.coroutineValue[i]);
            }

            Destroy(this.gameObject, 0.1f);
        }

        private void InitializeItem(Core core)
        {
            this.ItemCore = core;
        }
        /// <summary>
        /// 감지된 아이템
        /// </summary>
        /// <param name="isright">Detector의 오른쪽인지(Default true)</param>
        public void Detected(bool isright = true)
        {
            GameManager.Inst.SubUI.isRight(isright);
            GameManager.Inst.SubUI.DetailSubUI.GetComponent<DetailUI>().MainItemName = itemData.ItemData.ItemName;
            GameManager.Inst.SubUI.DetailSubUI.GetComponent<DetailUI>().SubItemName = itemData.ItemData.ItemDescription;

            if (GameManager.Inst.SubUI.DetailSubUI.gameObject.activeSelf)
            {
                //대충 SubUI 내용 바꾸는 코드                
                Debug.Log($"Change {GameManager.Inst.SubUI.DetailSubUI.gameObject.name} Text");
                GameManager.Inst.SubUI.SetSubUI(false);
                GameManager.Inst.SubUI.SetSubUI(true);
            }
            else
            {
                Debug.Log($"{GameManager.Inst.SubUI.DetailSubUI.gameObject.name} SetActive(true)");
                GameManager.Inst.SubUI.SetSubUI(true);
            }
        }

        public void UnDetected()
        {
            GameManager.Inst.SubUI.SetSubUI(false);
        }

        public void Conflict(string coroutine)
        {
            StartCoroutine(coroutine);
        }

        #region IEnumerator
        IEnumerator IncreaseHealth(float value)
        {
            if (unit != null)
            {
                unit.Core.GetCoreComponent<UnitStats>().IncreaseHealth(value);
            }
            yield return 0;
        }
        IEnumerator DetectedSense()
        {
            Debug.LogWarning($"DetectedSense {this.name}");
            if (itemData != null ? true : false)
            {
                if (itemData.AcquiredEffectPrefab != null)
                    Instantiate(itemData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity, particleContainer);
                Debug.LogWarning($"Get {this.name}");
                this.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("itemData is null");
            }
            yield return 0;
        }
        IEnumerable Conflict()
        {

            yield return 0;
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, DetectedRadius/2);
        }
    }
}

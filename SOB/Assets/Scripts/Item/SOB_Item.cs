using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;
using static UnityEngine.UI.CanvasScaler;

namespace SOB.Item
{
    public class SOB_Item : MonoBehaviour
    {
        public InventoryItem Item { get; private set; }
        public Core ItemCore { get; private set; }
        [HideInInspector] public Unit unit;

        [SerializeField]
        private float DetectedRadius;

        private CircleCollider2D CC2D;
        private Transform particleContainer;
        private void Awake()
        {
            particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
            Item = GetComponent<InventoryItem>();
        }
        private void OnEnable()
        {
            CC2D = GetComponentInChildren<CircleCollider2D>();
            if(CC2D != null)
            {
                CC2D.isTrigger = true;
                CC2D.radius = DetectedRadius;
            }

        }

        private void OnDisable()
        {            
            Destroy(this.gameObject, 5f);
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
            GameManager.Inst.SubUI.DetailSubUI.GetComponent<DetailUI>().MainItemName = Item.itemData.ItemData.ItemName;
            GameManager.Inst.SubUI.DetailSubUI.GetComponent<DetailUI>().SubItemName = Item.itemData.ItemData.ItemDescription;

            if (GameManager.Inst.SubUI.DetailSubUI.gameObject.activeSelf)
            {
                //대충 SubUI 내용 바꾸는 코드                
                Debug.Log($"Change {GameManager.Inst.SubUI.DetailSubUI.gameObject.name} Text");
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

        public void CallCoroutine(string coroutine)
        {
            //ifBuff
            if(unit.gameObject.GetComponent<BuffSystem>())
            {
                Buff buff = new Buff();
                buff.ItemData = Item.itemData;
                unit.gameObject.GetComponent<BuffSystem>().AddBuff(buff);
            }

            //Effect
            StartCoroutine(coroutine);
        }

        

        #region IEnumerator
        IEnumerator E_Increase(string statName , float value, float duration)
        {
            if (unit != null)
            {
                unit.Core.GetCoreComponent<UnitStats>().Increase(statName, value);
                yield return new WaitForSeconds(duration);
                unit.Core.GetCoreComponent<UnitStats>().Decrease(statName, value);
                Destroy(this.gameObject);
                yield break;
            }
            
            yield break;
        }

        IEnumerator E_Decrease(string statName, float value, float duration)
        {
            if (unit != null)
            {
                unit.Core.GetCoreComponent<UnitStats>().Decrease(statName, value);
                yield return new WaitForSeconds(duration);
                unit.Core.GetCoreComponent<UnitStats>().Increase(statName, value);
                Destroy(this.gameObject);
                yield break;
            }
            
            yield break;
        }
        IEnumerator E_Buff(float value)
        {
            if (unit != null)
            {
                unit.GetComponent<Inventory>().items.Add(new ItemDataSO());
            }
            yield return 0;
        }
        
        IEnumerator DetectedSense()
        {
            
            yield return 0;
        }
        IEnumerator Collision()
        {
            Debug.LogWarning($"Conflict {this.name}");
            if (Item.itemData != null ? true : false)
            {
                if (Item.itemData.AcquiredEffectPrefab != null)
                    Instantiate(Item.itemData.AcquiredEffectPrefab, this.gameObject.transform.position, Quaternion.identity, particleContainer);
                Debug.LogWarning($"Get {this.name}");
                //this.gameObject.SetActive(false);
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(CC2D.gameObject);
                yield return 0;
            }
            else
            {
                Debug.LogWarning("itemData is null");
            }
            yield return 0;
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            if (DetectedRadius != 0)
            {
                Gizmos.DrawWireSphere(transform.position, DetectedRadius/2);
            }
        }
    }
}

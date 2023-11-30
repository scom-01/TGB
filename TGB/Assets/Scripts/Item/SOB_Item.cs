using TGB.CoreSystem;
using UnityEngine;

namespace TGB.Item
{
    public class SOB_Item : MonoBehaviour
    {
        [field: SerializeField] public StatsItemSO Item;
        public Core ItemCore { get; private set; }
        [HideInInspector] public Unit unit;

        [SerializeField] private float DetectedRadius;

        private SpriteRenderer SR;
        private CircleCollider2D CC2D;
        private Transform effectContainer;
        private void Awake()
        {
            effectContainer = GameObject.FindGameObjectWithTag("EffectContainer").transform;
        }
        private void OnEnable()
        {
            SR = this.GetComponent<SpriteRenderer>();

            if(Init())
            {
                Debug.Log("Item Init");
            }
        }

        private void OnDisable()
        {
            Destroy(this.gameObject, 5f);
        }

        public bool Init()
        {
            if (Item == null)
                return false;

            SR.sprite = Item.itemData.ItemSprite;
            CC2D = GetComponentInChildren<CircleCollider2D>();
            if (CC2D != null)
            {
                CC2D.isTrigger = true;
                CC2D.radius = DetectedRadius;
            }
            return true;
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
            if (GameManager.Inst == null)
            {
                Debug.LogWarning("GameMamanger.Inst is Null");
                return;
            }

            GameManager.Inst.SubUI.isRight(isright);
            GameManager.Inst.SubUI.DetailSubUI.SetInit(Item);

            if (GameManager.Inst.SubUI.DetailSubUI.Canvas.enabled)
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

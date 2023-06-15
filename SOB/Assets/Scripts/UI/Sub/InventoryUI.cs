using System;
using Unity.VisualScripting;
using UnityEngine.Localization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SOB.Manager
{
    public class InventoryUI : MonoBehaviour
    {
        private Player Player
        {
            get
            {
                if(GameManager.Inst!=null)
                {
                    if(GameManager.Inst.StageManager!= null)
                    {
                        return GameManager.Inst.StageManager.player;
                    }
                }
                return null;
            }
        }
        private Inventory PlayerInventory
        {
            get
            {
                if(Player != null)
                {
                    return Player.Inventory;
                }
                return null;
            }
        }
        private PlayerInputHandler inputHandler
        { 
            get
            {
                if(GameManager.Inst==null)
                {
                    return null;
                }
                return GameManager.Inst.inputHandler;
            }
        }

        public event Action OnInteractionInput;

        public InventoryItems InventoryItems
        {
            get
            {
                if (inventoryItems == null)
                {
                    inventoryItems = this.GetComponentInChildren<InventoryItems>();
                }
                return inventoryItems;
            }
            set => inventoryItems = value;
        }
        public InventoryStats InventoryStats
        {
            get
            {
                if (inventoryStats == null)
                {
                    inventoryStats = this.GetComponentInChildren<InventoryStats>();
                }
                return inventoryStats;
            }
            set => inventoryStats = value;
        }
        public InventoryDescript InventoryDescript
        {
            get
            {
                if (inventoryDescript == null)
                {
                    inventoryDescript = this.GetComponentInChildren<InventoryDescript>();
                }
                return inventoryDescript;
            }
            set => inventoryDescript = value;
        }

        private InventoryItems inventoryItems;
        private InventoryStats inventoryStats;
        private InventoryDescript inventoryDescript;
        public Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponent<Canvas>();
                }
                return canvas;
            }
        }
        private Canvas canvas;

        private void Update()
        {
            InputCheck();
        }

        public void InputCheck()
        {
            if (inputHandler == null)
            {
                Debug.LogWarning("InventoryUI inputHandler is null");
                return;
            }

            if (inputHandler.InteractionInput)
            {
                OnInteractionInput?.Invoke();
            }
        }

        private void OnEnable()
        {
            if (PlayerInventory != null)
            {
                for (int i = 0; i < PlayerInventory._items.Count; i++)
                {
                    InventoryItems.items[i].StatsItemData = PlayerInventory._items[i].item;
                }
            }                
        }

        private void OnDisable()
        {
            PutInventoryItem();
        }

        public bool NullCheckInput()
        {
            OnInteractionInput = null;
            return true;
        }

        /// <summary>
        /// InteractionInput 시 인벤토리아이템을 떨어뜨리는 상태로 전환
        /// </summary>
        public void PutInventoryItem()
        {
            //Input 초기화
            NullCheckInput();
            OnInteractionInput += PutItem;
        }

        /// <summary>
        /// InteractionInput 시 인벤토리을 교체하는 상태로 전환
        /// </summary>
        public void ChangeInventoryItem()
        {
            //Input 초기화
            NullCheckInput();
            OnInteractionInput += ChangeItem;
        }

        public void PutItem()
        {

            if (this.InventoryItems.CurrentSelectItem == null)
            {
                Debug.Log("선택된 아이템 없음");
                return;
            }
            if (!PlayerInventory.RemoveInventoryItem(this.InventoryItems.CurrentSelectItem.StatsItemData))
                return;
            this.InventoryItems.CurrentSelectItemIndex--;
            EventSystem.current.SetSelectedGameObject(this.InventoryItems.CurrentSelectItem.gameObject);
            GameManager.Inst.SubUI.InventorySubUI.InventoryDescript.SetDescript();
            inputHandler.UseInput(ref inputHandler.InteractionInput);
        }

        public void ChangeItem()
        {
            if (this.InventoryItems.CurrentSelectItem == null)
            {
                Debug.Log("선택된 아이템 없음");
                return;
            }
            if (!PlayerInventory.RemoveInventoryItem(this.InventoryItems.CurrentSelectItem.StatsItemData))
                return;
            EventSystem.current.SetSelectedGameObject(this.InventoryItems.CurrentSelectItem.gameObject);
            GameManager.Inst.SubUI.InventorySubUI.InventoryDescript.SetDescript();
            PlayerInventory.AddInventoryItem(PlayerInventory.CheckItem);
            Destroy(PlayerInventory.CheckItem.GameObject());
            PlayerInventory.CheckItem = null;
            inputHandler.UseInput(ref inputHandler.InteractionInput);
            GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, true);
        }
    }
}

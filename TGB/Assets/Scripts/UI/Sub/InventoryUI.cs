using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum InventoryUI_State
{
    Put,
    Change,
}

namespace SCOM.Manager
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
                return GameManager.Inst.InputHandler;
            }
        }


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
        public EquipWeapon EquipWeapon
        {
            get
            {
                if (equipWeapon == null)
                {
                    equipWeapon = this.GetComponentInChildren<EquipWeapon>();
                }
                return equipWeapon;
            }
            set => equipWeapon = value;
        }

        private InventoryItems inventoryItems;
        private InventoryDescript inventoryDescript;
        private EquipWeapon equipWeapon;

        public event Action OnInteractionInput;
        private InventoryUI_State State = InventoryUI_State.Put;
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
        public Animator animator
        {
            get
            {
                if (anim == null)
                {
                    anim = GetComponent<Animator>();
                }
                return anim;
            }
        }
        private Animator anim;
        private void Update()
        {
            if (!Canvas.enabled)
                return;

            InputCheck();
        }

        public void InputCheck()
        {
            if (inputHandler == null)
            {
                Debug.LogWarning("InventoryUI inputHandler is null");
                return;
            }
            if(inputHandler.playerInput.currentActionMap != inputHandler.playerInput.actions.FindActionMap(InputEnum.UI.ToString()))
            {
                return;
            }
            if (inputHandler.InteractionInput && inputHandler.interactionTap)
            {
                ChangeInventoryState(State);
                OnInteractionInput?.Invoke();
                inputHandler.UseInput(ref inputHandler.InteractionInput);
                inputHandler.UseInput(ref inputHandler.interactionTap);
            }
        }

        private void OnEnable()
        {
            if (PlayerInventory != null)
            {
                for (int i = 0; i < PlayerInventory.Items.Count; i++)
                {
                    InventoryItems.Items[i].StatsItemData = PlayerInventory.Items[i].item;
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

        public void ChangeInventoryState(InventoryUI_State state)
        {
            NullCheckInput();
            switch (state)
            {
                case InventoryUI_State.Put:
                    PutInventoryItem();
                    break;
                case InventoryUI_State.Change:
                    ChangeInventoryItem();
                    break;
            }
        }

        public void SetInventoryState(InventoryUI_State state)
        {
            State = state;
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

        /// <summary>
        /// 선택된 아이템(CurrentSelectItem) 드랍
        /// </summary>
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

        /// <summary>
        /// 아이템 변경
        /// 선택된 아이템(CurrentSelectItem)을 바닥에 드랍하고 변경할 아이템(CheckItem)을 장착한다.
        /// </summary>
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
            GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, true);
        }
    }
}

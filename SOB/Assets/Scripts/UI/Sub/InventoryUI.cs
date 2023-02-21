using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

namespace SOB.Manager
{
    public class InventoryUI : MonoBehaviour
    {
        private Player Player;
        private Inventory PlayerInventory;
        private PlayerInputHandler inputHandler;

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
            set =>  inventoryDescript = value;
        }

        private InventoryItems inventoryItems;
        private InventoryStats inventoryStats;
        private InventoryDescript inventoryDescript;

        private void Awake()
        {
            Player = GameManager.Inst.player;
            inputHandler = GameManager.Inst.inputHandler;
            PlayerInventory = Player.GetComponent<Inventory>();
        }
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

            if (inputHandler.RawUIMoveInputRight)
            {
                InventoryItems.CurrentSelectItemIndex++;
                inputHandler.UseInput(ref inputHandler.RawUIMoveInputRight);
            }
            else if (inputHandler.RawUIMoveInputLeft)
            {
                InventoryItems.CurrentSelectItemIndex--;
                inputHandler.UseInput(ref inputHandler.RawUIMoveInputLeft);
            }

            if (inputHandler.InteractionInput)
            {
                Debug.LogWarning("inputHandler InteractionInput ");
                if (this.InventoryItems.CurrentSelectItem == null)
                {
                    return;
                }

                PlayerInventory.RemoveInventoryItem(this.InventoryItems.CurrentSelectItem.StatsItemData);
                this.InventoryItems.CurrentSelectItemIndex--;
                inputHandler.UseInput(ref inputHandler.InteractionInput);
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < PlayerInventory.items.Count; i++)
            {
                InventoryItems.items[i].StatsItemData = PlayerInventory.items[i];
            }
        }

    }
}

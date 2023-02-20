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

        [HideInInspector] public InventoryItems InventoryItems;
        [HideInInspector] public InventoryStats InventoryStats;
        [HideInInspector] public InventoryDescript InventoryDescript;

        private void Awake()
        {
            Player = GameManager.Inst.player;
            inputHandler = GameManager.Inst.inputHandler;
            PlayerInventory = Player.GetComponent<Inventory>();
        }
        private void Update()
        {
            if(inputHandler.RawUIMoveInputRight)
            {
                Debug.Log("Right");
                InventoryItems.CurrentSelectItemIndex++;
            }
            else if(inputHandler.RawUIMoveInputLeft)
            {
                Debug.Log("Left");
                InventoryItems.CurrentSelectItemIndex--;
            }
        }


        private void OnEnable()
        {
            InventoryItems = this.GetComponentInChildren<InventoryItems>();
            InventoryStats = this.GetComponentInChildren<InventoryStats>();
            InventoryDescript = this.GetComponentInChildren<InventoryDescript>();

            for (int i = 0; i < PlayerInventory.items.Count; i++)
            {
                InventoryItems.items[i].StatsItemData = PlayerInventory.items[i];
            }
        }

    }
}

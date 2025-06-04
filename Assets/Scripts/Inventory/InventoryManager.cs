using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class InventoryManager: MonoBehaviour
    {
        private int selectedSlot = 0;
        [SerializeField] private InventorySlot[] inventorySlots;
        [SerializeField] private Seed grassSeed;
        [SerializeField] private Hoe hoeTool;
        [SerializeField] private GameObject inventoryItemPrefab;
        [SerializeField]private GameObject inventoryUI;

        private bool isInventoryOpen = false;

        public bool IsInventoryOpen
        {
            get { return isInventoryOpen; }
            
        }
        
        
        public void ToggleInventory()
        {
            if (isInventoryOpen)
            {
                inventoryUI.SetActive(false);
                isInventoryOpen = false;
            }
            else
            {
                inventoryUI.SetActive(true);
                isInventoryOpen = true;
            }
        }
        public void AddItem(Item item, int amount)
        {
            // First, try to stack the item in an existing slot
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                if (slot.HasItem && slot.IsSameItem(item) && slot.NewStackFits(amount))
                {
                    slot.AddStack(amount);
                    return;
                }
            }

            // If not stackable, try to add to an empty slot
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                if (!slot.HasItem)
                {
                    SpawnItem(item, slot, amount);
                    return;
                }
            }
        }

        void Start()
        {
            AddItem(grassSeed, 1);
            AddItem(grassSeed, 1);
            AddItem(grassSeed, 1);
            AddItem(hoeTool, 1);
            inventorySlots[selectedSlot].SetSelected(true);
        }

        public void SpawnItem(Item item, InventorySlot slot, int amount)
        {
            GameObject itemPrefab = Instantiate(inventoryItemPrefab, slot.transform);
            InventoryItem inventoryItem = itemPrefab.GetComponent<InventoryItem>();
            inventoryItem.Initialize(item);
            slot.AddNewItem(inventoryItem, amount);
        }

        public void RemoveSelectedItem(int amount)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            if (slot.HasItem)
            {
                InventoryItem inventoryItem = slot.InventoryItem;
                if (inventoryItem.Amount > amount)
                {
                    inventoryItem.Amount -= amount;
                }
                else
                {
                    Destroy(inventoryItem.gameObject);
                    slot.ClearSlot();
                }
            }
        }

        public void ChangeHotbarItem(int changeAmount)
        {
            int hotbarSize = 10;
            inventorySlots[selectedSlot].SetSelected(false);
            selectedSlot = (selectedSlot + changeAmount + hotbarSize) % hotbarSize;
            inventorySlots[selectedSlot].SetSelected(true);
    
        }

        public Item GetHeldItem()
        {
            if (inventorySlots[selectedSlot].HasItem)
            {
                return inventorySlots[selectedSlot].InventoryItem.Item;
            }
            return null;
        }

        public void UseHeldItem(GameObject user, Vector2 position)
        {
            Item item = GetHeldItem();
            if (item != null)
            {
                if (item.Use(user, position) && item.IsConsumable)
                    RemoveSelectedItem(1);
            }
            else
            {
                Debug.Log("No item to use.");
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class InventoryManager
    {
        private int currentItemIndex = 0;
        private static InventoryManager _instance;
        public List<Item> inventory = new List<Item>();
        private InventoryManager() {}
        public static InventoryManager Instance
        {
            get
            {
                _instance ??= new InventoryManager();
                return _instance;
            }
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
        }

        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
        }

        public void SetHeldItem(int index)
        {
            if (index >= 0 && index < inventory.Count)
            {
                currentItemIndex = index;
            }
        }

        public void ChangeItem(int changeAmount)
        {
            if (inventory.Count > 0)
            {
            currentItemIndex = (currentItemIndex + changeAmount + inventory.Count) % inventory.Count;
            }
        }

        public Item GetHeldItem()
        {
            if (inventory.Count > 0)
            {
                return inventory[currentItemIndex];
            }
            return null;
        }

        public void UseHeldItem(GameObject user, Vector2 position)
        {
            Item item = GetHeldItem();
            if (item != null)
            {
                item.Use(user, position);
                RemoveItem(item);
            }
            else
            {
                Debug.Log("No item to use.");
            }
        }
    }
}
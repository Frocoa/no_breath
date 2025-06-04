using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public bool HasItem => transform.childCount > 0;
        private Image background;

        [SerializeField] Color selectedColor, unselectedColor;

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                background.color = selectedColor; // Highlight color
            }
            else
            {
                background.color = unselectedColor; // Default color
            }
        }

        void Awake()
        {
            background = GetComponent<Image>();
            // background.color = unselectedColor;
        }

        public InventoryItem InventoryItem
        {
            get
            {
                if (HasItem)
                    return GetInventoryItem();
                return null;
            }
        }

        public void ClearSlot()
        {
            if (HasItem)
            {
                InventoryItem inventoryItem = GetInventoryItem();
                if (inventoryItem != null)
                {
                    Destroy(inventoryItem.gameObject);
                }
            }
        }

        public bool IsSameItem(Item item)
        {
            return HasItem && GetInventoryItem().Item == item;
        }

        public bool NewStackFits(int newStackSize)
        {
            if (!HasItem) return true;
            var inventoryItem = GetInventoryItem();
            return inventoryItem.Amount + newStackSize <= inventoryItem.Item.MaxStack;
        }

        private InventoryItem GetInventoryItem()
        {
            if (!HasItem) return null;
            return transform.GetChild(0).GetComponent<InventoryItem>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            // Debug.Log($"Dropped on slot: {gameObject.name}");
            if (!HasItem)
            {
                var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                if (inventoryItem != null)
                    inventoryItem.parentAfterDrag = transform;
            }
        }

        public void AddNewItem(InventoryItem item, int amount)
        {
            if (item != null)
                item.transform.SetParent(transform);
                item.Amount = amount;
        }

        public void AddStack(int amount)
        {
            var invItem = InventoryItem;
            if (invItem != null)
            {
                invItem.Amount += amount;
                if (invItem.Amount > invItem.Item.MaxStack)
                    invItem.Amount = invItem.Item.MaxStack;
            }
        }
    }
}

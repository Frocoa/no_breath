using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int maxStack = 2;

        public string ItemName => itemName;
        public Sprite Icon => icon;
        public int MaxStack => maxStack;

        public abstract void Use(GameObject player);
        public abstract void Use(GameObject player, Vector2 position);
    }
}

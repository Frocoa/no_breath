using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int maxStack = 2;
        [SerializeField] private bool isConsumable = true;

        public bool IsConsumable => isConsumable;
        public string ItemName => itemName;
        public Sprite Icon => icon;
        public int MaxStack => maxStack;
        public abstract bool IsUsable(GameObject player, Vector2 position);
        public abstract void UseCore(GameObject player, Vector2 position);
        public bool Use(GameObject player, Vector2 position)
        {
            if (IsUsable(player, position))
            {
                UseCore(player, position);
                return true;
            }
            return false;
        }
        public bool Use(GameObject player)
        {
            return Use(player, player.transform.position);
        }
    }
}

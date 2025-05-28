using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField]
        private string itemName;

        public string ItemName => itemName;

        public abstract void Use(GameObject player);
        public abstract void Use(GameObject player, Vector2 position);
    }
}

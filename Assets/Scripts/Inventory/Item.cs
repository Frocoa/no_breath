using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public abstract class Item
    {
        protected string name;

        public Item(string name)
        {
            this.name = name;
        }
        
        public abstract void Use();
    }
}
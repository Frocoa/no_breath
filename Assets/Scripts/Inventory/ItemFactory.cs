using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Inventory
{
    public class ItemFactory : MonoBehaviour {

        public static GameObject SpawnItem(GameObject prefab, Vector3 position)
        {
            Vector3 cellPosition = MainGrid.Instance.WorldToCell(position);
            GameObject obj = Instantiate(prefab, cellPosition, Quaternion.identity);
            return obj;
        }
        public static GameObject SpawnItemWorld(GameObject prefab, Vector3 position)
        {
            Vector3 cellPosition = MainGrid.Instance.WorldToCell(position);
            GameObject obj = Instantiate(prefab, cellPosition, Quaternion.identity);
            return obj;
        }
    }
}
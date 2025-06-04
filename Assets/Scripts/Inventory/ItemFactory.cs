using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Inventory
{
    public class ItemFactory : MonoBehaviour {

        public static Crop SpawnItem(Crop prefab, Vector3 position)
        {
            Vector3 cellPosition = MainGrid.Instance.WorldToCell(position);
            Crop obj = Instantiate(prefab, cellPosition, Quaternion.identity);
            obj.Initialize(MainGrid.Instance.GetObjectsTileMap(), position);
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
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Inventory
{
    public class ItemFactory : MonoBehaviour {

        [SerializeField]
        private Tilemap objectTilemap;
        public static ItemFactory Instance;

        private void Awake() {
            // Singleton básico
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private Vector3 WorldToCell(Vector3 worldPosition)
        {
            Vector3Int cellPosition = objectTilemap.WorldToCell(worldPosition);
            return objectTilemap.GetCellCenterWorld(cellPosition);
        }
        public GameObject SpawnItem(GameObject prefab, Vector3 position)
        {
            Vector3 cellPosition = WorldToCell(position);
            GameObject obj = Instantiate(prefab, cellPosition, Quaternion.identity);
            return obj;
        }
    }
}
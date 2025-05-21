using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemFactory : MonoBehaviour {
        // Implementación del singleton para facilitar el acceso global
        public static ItemFactory Instance;

        private void Awake() {
            // Singleton básico
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public GameObject SpawnItem(GameObject prefab, Vector3 position) {
            GameObject obj = Instantiate(prefab, position, Quaternion.identity);
            return obj;
        }
    }
}
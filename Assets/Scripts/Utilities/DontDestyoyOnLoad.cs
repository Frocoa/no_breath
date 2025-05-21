using UnityEngine;

namespace Utilities {
    public class DontDestyoyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


namespace Assets.Scripts.Player
{
  
    public class CharacterInputcontroller : MonoBehaviour
    {

        InventoryManager inventoryManager;
        
        [SerializeField]
        Seed grassSeed;

        public void Awake()
        {
            inventoryManager = InventoryManager.Instance;
            inventoryManager.AddItem(grassSeed);
            
        }

        [SerializeField] 
        private float speed = 10f;

        private Vector2 moveInput;
        private void Update()
        {
            transform.position += (Vector3)moveInput * Time.deltaTime;
        }
        public void Move(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) {
                moveInput = context.ReadValue<Vector2>() * speed;
            }
            else if (context.phase == InputActionPhase.Canceled) {
                moveInput = Vector2.zero;
            }
        }

        public void UseItem(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                inventoryManager.UseHeldItem(gameObject);
            }
            
        }

        public void ScrollItem(InputAction.CallbackContext context) 
        {
            if (context.phase == InputActionPhase.Performed) {
                Debug.Log("Scrolling item");
                float scrollValue = context.ReadValue<float>();
                if (scrollValue > 0) {
                    inventoryManager.ChangeItem(1);
                } else if (scrollValue < 0) {
                    inventoryManager.ChangeItem(-1);
                }
            }

        }
    }
}

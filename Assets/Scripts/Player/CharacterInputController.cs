using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterInputcontroller : MonoBehaviour
    {
        InventoryManager inventoryManager;

        [SerializeField] private Seed grassSeed;
        [SerializeField] private float speed = 10f;

        private Vector2 moveInput;
        private Rigidbody2D rb;

        private void Awake()
        {
            inventoryManager = InventoryManager.Instance;
            inventoryManager.AddItem(grassSeed);

            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // Mover con física
            rb.MovePosition(rb.position + moveInput * Time.fixedDeltaTime);
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                moveInput = context.ReadValue<Vector2>() * speed;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                moveInput = Vector2.zero;
            }
        }

        public void UseItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                inventoryManager.UseHeldItem(gameObject);
            }
        }

        public void ScrollItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                float scrollValue = context.ReadValue<float>();
                if (scrollValue > 0)
                    inventoryManager.ChangeItem(1);
                else if (scrollValue < 0)
                    inventoryManager.ChangeItem(-1);
            }
        }
    }
}
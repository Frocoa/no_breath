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

        [SerializeField] private Grid grid;

        private Vector2 moveInput;
        private Vector2 mousePosition;
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

                inventoryManager.UseHeldItem(gameObject, mousePosition);
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
        public void MousePositionUpdate(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Vector2 mousePositionScreen = context.ReadValue<Vector2>();
                mousePosition = Camera.main.ScreenToWorldPoint(mousePositionScreen);
            }
        }

        public bool HasItemInHand()
        {
            return inventoryManager.GetHeldItem() != null;
        }
        
        public Vector2 GetMousePosition()
        {
            return mousePosition;
        }
    }
}
using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputcontroller : MonoBehaviour
{

    InventoryManager inventoryManager;
    
    [SerializeField]
    GameObject plantPrefab;

    public void Awake()
    {
        inventoryManager = InventoryManager.Instance;
        //inventoryManager.AddItem(new Seed("Semilla", plantPrefab));

        TickManager.Instance.SubscribeToRandomTick(() => {
            Debug.Log("Random Tick");

        });
        
    }

    [SerializeField] 
    private float speed = 50f;

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

    public void UseItem() {
        Item item = inventoryManager.GetHeldItem();
        if (item != null) {
            item.Use();
        } else {
            Debug.Log("No item to use.");
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

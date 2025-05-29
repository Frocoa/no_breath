using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;


namespace Assets.Scripts.Inventory
{

    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        private Item item;

        public Item Item => item;
        private Image image;
        [HideInInspector] public Transform parentAfterDrag;

        private TMP_Text text;

        private int amount = 1;

        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                text.text = amount == 1 ? "" : amount.ToString();
            }
        }

        void Awake()
        {
            image = GetComponent<Image>();
            text = GetComponentInChildren<TMP_Text>();
        }
        public void Initialize(Item item)
        {
            this.item = item;
            image.sprite = item.Icon;
            text.text = amount == 1 ? "" : amount.ToString();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            Debug.Log("Drag started on: " + gameObject.name);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
            transform.position = worldPosition;
            Debug.Log("Dragging: " + gameObject.name);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            Debug.Log("Drag ended on: " + gameObject.name);
        }
    }
}

using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField]
    private UnityEngine.UI.Image image;

    [HideInInspector] public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false; // Desactivar raycast para permitir el arrastre
        parentAfterDrag = transform.parent; // Guardar el padre original
        transform.SetParent(transform.root); // Mover el objeto al root para evitar problemas de jerarquía
        Debug.Log("Drag started on: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Usar el nuevo Input System para obtener la posición del mouse
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        transform.position = worldPosition;
        Debug.Log("Dragging: " + gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true; // Reactivar raycast al finalizar el arrastre
        transform.SetParent(parentAfterDrag); // Volver al padre original
        Debug.Log("Drag ended on: " + gameObject.name);
        
        // Aquí podrías agregar lógica para manejar la colocación del objeto en el inventario
        // Por ejemplo, verificar si se soltó sobre un área válida o no.
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int number;

    private Transform originalParent;

    public void OnBeginDrag(PointerEventData e)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData e)
    {
        transform.position = e.position;
    }

    public void OnEndDrag(PointerEventData e)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
        }
    }
}
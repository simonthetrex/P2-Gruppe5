using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int number;

    private Transform originalParent;
    private Vector3 originalPos;

    public void OnBeginDrag(PointerEventData e)
    {
        originalParent = transform.parent;
        originalPos = transform.position;
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
            transform.position = originalPos;
        }
    }
}
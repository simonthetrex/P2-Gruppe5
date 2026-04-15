using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    public int correctNumber;
    private bool solved = false;

    public void OnDrop(PointerEventData e)
    {
        if (solved) return;

        DragHandler drag = e.pointerDrag.GetComponent<DragHandler>();
        if (drag == null) return;

        if (drag.number == correctNumber)
        {
            solved = true;

            drag.GetComponent<Image>().color = Color.green;
            drag.transform.SetParent(transform);
            RectTransform rect = drag.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.anchoredPosition = Vector2.zero;

            FindObjectOfType<GameManager>().TowerSolved();
        }
    }
}
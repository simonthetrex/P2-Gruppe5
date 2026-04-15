using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeButtonRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform drawingArea;
    [SerializeField] private float radiusScale = 0.33f;
    [SerializeField] private float lineThickness = 12f;
    [SerializeField] private float pointSize = 28f;
    [SerializeField] private Color lineColor = Color.black;
    [SerializeField] private Color pointColor = Color.red;

    private readonly List<GameObject> renderedElements = new List<GameObject>();

    private RectTransform EffectiveArea
    {
        get
        {
            if (drawingArea != null)
            {
                return drawingArea;
            }

            return transform as RectTransform;
        }
    }

    public void RenderShape(int sideCount)
    {
        ClearRenderedShape();

        if (sideCount < 3)
        {
            return;
        }

        RectTransform area = EffectiveArea;
        if (area == null)
        {
            return;
        }

        float radius = Mathf.Min(area.rect.width, area.rect.height) * radiusScale;
        Vector2[] points = new Vector2[sideCount];

        for (int i = 0; i < sideCount; i++)
        {
            float angle = Mathf.Deg2Rad * (90f - i * (360f / sideCount));
            points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        for (int i = 0; i < sideCount; i++)
        {
            int next = (i + 1) % sideCount;
            CreateLine(points[i], points[next], area);
        }

        for (int i = 0; i < sideCount; i++)
        {
            CreatePoint(points[i], area);
        }
    }

    private void ClearRenderedShape()
    {
        for (int i = 0; i < renderedElements.Count; i++)
        {
            if (renderedElements[i] != null)
            {
                Destroy(renderedElements[i]);
            }
        }

        renderedElements.Clear();
    }

    private void CreateLine(Vector2 from, Vector2 to, RectTransform parent)
    {
        GameObject lineObject = new GameObject("ShapeLine", typeof(RectTransform), typeof(Image));
        lineObject.transform.SetParent(parent, false);

        RectTransform lineRect = lineObject.GetComponent<RectTransform>();
        Image lineImage = lineObject.GetComponent<Image>();

        Vector2 direction = to - from;
        float length = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        lineRect.anchorMin = new Vector2(0.5f, 0.5f);
        lineRect.anchorMax = new Vector2(0.5f, 0.5f);
        lineRect.pivot = new Vector2(0.5f, 0.5f);
        lineRect.anchoredPosition = (from + to) * 0.5f;
        lineRect.sizeDelta = new Vector2(length, lineThickness);
        lineRect.localRotation = Quaternion.Euler(0f, 0f, angle);

        lineImage.color = lineColor;

        renderedElements.Add(lineObject);
    }

    private void CreatePoint(Vector2 position, RectTransform parent)
    {
        GameObject pointObject = new GameObject("ShapePoint", typeof(RectTransform), typeof(Image));
        pointObject.transform.SetParent(parent, false);

        RectTransform pointRect = pointObject.GetComponent<RectTransform>();
        Image pointImage = pointObject.GetComponent<Image>();

        pointRect.anchorMin = new Vector2(0.5f, 0.5f);
        pointRect.anchorMax = new Vector2(0.5f, 0.5f);
        pointRect.pivot = new Vector2(0.5f, 0.5f);
        pointRect.anchoredPosition = position;
        pointRect.sizeDelta = new Vector2(pointSize, pointSize);

        pointImage.color = pointColor;

        renderedElements.Add(pointObject);
    }
}

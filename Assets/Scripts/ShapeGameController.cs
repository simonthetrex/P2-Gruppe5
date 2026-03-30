using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShapeGameController : MonoBehaviour
{
    [SerializeField] private PolygonGenerator generator;
    [SerializeField] private Button[] shapeButtons;
    [SerializeField] private ShapeButtonRenderer[] shapeRenderers;
    [SerializeField] private TMP_Text targetShapeText;
    [SerializeField] private TMP_Text feedbackText;

    private void Awake()
    {
        if (shapeRenderers == null || shapeRenderers.Length != shapeButtons.Length)
        {
            shapeRenderers = new ShapeButtonRenderer[shapeButtons.Length];
        }

        for (int i = 0; i < shapeButtons.Length; i++)
        {
            int buttonIndex = i;
            shapeButtons[i].onClick.AddListener(() => OnShapeClicked(buttonIndex));

            if (shapeRenderers[i] == null)
            {
                shapeRenderers[i] = shapeButtons[i].GetComponent<ShapeButtonRenderer>();
            }

            if (shapeRenderers[i] == null)
            {
                shapeRenderers[i] = shapeButtons[i].gameObject.AddComponent<ShapeButtonRenderer>();
            }
        }

        SetAnswerButtonsInteractable(false);
    }

    public void StartRound()
    {
        generator.GenerateShapes();
        UpdateButtonShapes();
        targetShapeText.text = $"Find figuren med {generator.GetCorrectShape()} sider";
        feedbackText.text = string.Empty;
        SetAnswerButtonsInteractable(true);
    }

    public void OnShapeClicked(int index)
    {
        bool correct = generator.IsCorrectShape(index);
        feedbackText.text = correct ? "Rigtigt svar!" : "Forkert svar...";
        SetAnswerButtonsInteractable(false);
    }

    private void UpdateButtonShapes()
    {
        for (int i = 0; i < shapeButtons.Length; i++)
        {
            if (shapeRenderers[i] != null)
            {
                shapeRenderers[i].RenderShape(generator.GetDisplayedShape(i));
            }
        }
    }

    private void SetAnswerButtonsInteractable(bool canClick)
    {
        for (int i = 0; i < shapeButtons.Length; i++)
        {
            shapeButtons[i].interactable = canClick;
        }
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormerTælningController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ShapeButtonRenderer shapeRenderer;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TMP_Text startButtonText;

    [Header("Scener")]
    [SerializeField] private string gameSceneName = "Former";

    private int currentSideCount;
    private bool hasAnsweredCurrentShape;

    private void Awake()
    {
        if (answerButtons == null || answerButtons.Length == 0)
        {
            Debug.LogWarning("FormerTælningController: answerButtons er tom. Tilføj 4 knapper (3-6) i Inspector.");
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int sideCount = i + 3;
            Button button = answerButtons[i];

            if (button == null)
            {
                continue;
            }

            button.onClick.AddListener(() => OnAnswerClicked(sideCount));

            TMP_Text label = button.GetComponentInChildren<TMP_Text>();
            if (label != null)
            {
                label.text = GetShapeLabel(sideCount);
            }
        }

        if (shapeRenderer == null)
        {
            Debug.LogWarning("FormerTælningController: shapeRenderer mangler i Inspector.");
        }
    }

    private void Start()
    {
        GeneratePracticeShape();
    }

    public void StartPractice()
    {
        GeneratePracticeShape();
    }

    public void GeneratePracticeShape()
    {
        currentSideCount = UnityEngine.Random.Range(3, 7);
        hasAnsweredCurrentShape = false;

        if (shapeRenderer != null)
        {
            shapeRenderer.RenderShape(currentSideCount);
        }

        if (instructionText != null)
        {
            instructionText.text = "Tæl kanterne på figuren";
        }

        if (feedbackText != null)
        {
            feedbackText.text = string.Empty;
        }

        SetAnswerButtonsInteractable(true);
        UpdatePracticeButtonLabel();
    }

    public void OnAnswerClicked(int selectedSideCount)
    {
        bool isCorrect = selectedSideCount == currentSideCount;
        hasAnsweredCurrentShape = true;

        if (feedbackText != null)
        {
            feedbackText.text = isCorrect
                ? $"Rigtigt! Figuren har {currentSideCount} kanter."
                : $"Ikke helt. Figuren har {currentSideCount} kanter.";
        }

        SetAnswerButtonsInteractable(false);
        UpdatePracticeButtonLabel();
    }

    public void OnAnswer3Clicked()
    {
        OnAnswerClicked(3);
    }

    public void OnAnswer4Clicked()
    {
        OnAnswerClicked(4);
    }

    public void OnAnswer5Clicked()
    {
        OnAnswerClicked(5);
    }

    public void OnAnswer6Clicked()
    {
        OnAnswerClicked(6);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void SetAnswerButtonsInteractable(bool canClick)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
            {
                answerButtons[i].interactable = canClick;
            }
        }
    }

    private string GetShapeLabel(int sideCount)
    {
        switch (sideCount)
        {
            case 3:
                return "3 " + Environment.NewLine + "(Trekant)";
            case 4:
                return "4 " + Environment.NewLine + "(Firkant)";
            case 5:
                return "5 " + Environment.NewLine + "(Femkant)";
            case 6:
                return "6 " + Environment.NewLine + "(Sekskant)";
            default:
                return sideCount.ToString();
        }
    }

    private void UpdatePracticeButtonLabel()
    {
        if (startButtonText == null)
        {
            return;
        }

        startButtonText.text = hasAnsweredCurrentShape ? "Ny form" : "Spring over";
    }
}

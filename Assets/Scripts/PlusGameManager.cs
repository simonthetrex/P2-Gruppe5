using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusGameManager : MonoBehaviour
{
    public GameObject fishPrefab;

    public TextMeshProUGUI feedbackText;

    public Transform leftPanel;
    public Transform rightPanel;

    public AnswerButton[] buttons;

    private int correctAnswer;

    void Start()
    {
        GenerateQuestion();
    }

    public void GenerateQuestion()
    {
        feedbackText.text = "";

        int left = Random.Range(1, 6);
        int right = Random.Range(1, 6);

        correctAnswer = left + right;

        SpawnFish(leftPanel, left);
        SpawnFish(rightPanel, right);

        SetupAnswers();
    }

    void SpawnFish(Transform panel, int amount)
    {
        // Clear old fish
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }

        // Spawn new fish
        for (int i = 0; i < amount; i++)
        {
            Instantiate(fishPrefab, panel);
        }
    }

    void SetupAnswers()
    {
        List<int> answers = new List<int>();
        answers.Add(correctAnswer);

        // Generate wrong answers
        while (answers.Count < buttons.Length)
        {
            int wrong = correctAnswer + Random.Range(-3, 4);

            if (wrong > 0 && !answers.Contains(wrong))
            {
                answers.Add(wrong);
            }
        }

        // Shuffle answers
        for (int i = 0; i < answers.Count; i++)
        {
            int rand = Random.Range(0, answers.Count);
            int temp = answers[i];
            answers[i] = answers[rand];
            answers[rand] = temp;
        }

        // Send values to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetAnswer(
                answers[i],
                answers[i] == correctAnswer
            );
        }
    }

    public void ShowCorrect()
    {
        feedbackText.text = "Correct!";
        feedbackText.color = Color.green;
    }

    public void ShowWrong()
    {
        feedbackText.text = "Wrong!";
        feedbackText.color = Color.red;
    }

    public IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(1.5f);
        GenerateQuestion();
    }
}
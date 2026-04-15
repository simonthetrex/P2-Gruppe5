using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public Transform fishContainer;
    public GameObject fishPrefab;

    private bool isCorrect;
    private int value;

    public PlusGameManager gameManager;

    public void SetAnswer(int number, bool correct)
    {
        value = number;
        isCorrect = correct;

        DisplayFish(number);
    }

    void DisplayFish(int amount)
    {
        // Clear old fish
        foreach (Transform child in fishContainer)
        {
            Destroy(child.gameObject);
        }

        // Spawn new fish
        for (int i = 0; i < amount; i++)
        {
            Instantiate(fishPrefab, fishContainer);
        }
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            gameManager.ShowCorrect();
            StartCoroutine(gameManager.NextQuestionDelay());
        }
        else
        {
            gameManager.ShowWrong();
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class TælningLevels : MonoBehaviour
{

    public bool answered = false;

    public List<int> numbers = new List<int>{1,2,3,4,5};

    public List<int> answerIndex = new List<int>{0,0,0,0,0};

    public Button[] buttons;

    public int numberToGuess;
    
    public int randomAnswer;

    public int correctAnswerIndex;
    
    private Button lastClickedButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject buttonsParent = GameObject.Find("buttons");
        if (buttonsParent != null)
        {
            buttons = buttonsParent.GetComponentsInChildren<Button>();
        }
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (lastClickedButton != null)
        {
            lastClickedButton.GetComponent<Image>().color = Color.white;
        }

        numberToGuess = Random.Range(1, numbers.Count+1);
        correctAnswerIndex = Random.Range(0, answerIndex.Count);
        for (int i = 0; i < answerIndex.Count; i++)
        {
            if (i == correctAnswerIndex)
            {
                answerIndex[i] = numberToGuess;
            }
            else
            {
                bool tryingOriginalNumber = true;
                while (tryingOriginalNumber)
                {
                    randomAnswer = Random.Range(0, 8);
                    if (!answerIndex.Contains(randomAnswer) && randomAnswer != numberToGuess)
                    {
                        answerIndex[i] = randomAnswer;
                        tryingOriginalNumber = false;
                    }
                }
            }
        }
        for (int x = 0; x < buttons.Length; x++)
        {
            TextMeshProUGUI buttonTextComponent = buttons[x].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextComponent.text = answerIndex[x].ToString();
        }
        numbers.Remove(numberToGuess);
    }

    public void CheckAnswer(Button clickedButton)
    {
        lastClickedButton = clickedButton;
        TextMeshProUGUI clickedText = clickedButton.GetComponentInChildren<TextMeshProUGUI>();

        if (clickedText.text == numberToGuess.ToString())
        {
            clickedButton.GetComponent<Image>().color = Color.green;
            StartCoroutine(animate(3.0f));
        }
        else
        {
            clickedButton.GetComponent<Image>().color = Color.red;
            StartCoroutine(animate(3.0f));
        }
    }

    private IEnumerator animate(float time)
    {
        numbers.Insert(numberToGuess-1, numberToGuess);
        yield return new WaitForSeconds(time);
        NextQuestion();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

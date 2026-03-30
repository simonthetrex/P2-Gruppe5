using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TælningLevels : MonoBehaviour
{

    public bool answered = false;

    public List<int> numbers = new List<int>{1,2,3,4,5};

    public List<int> answerIndex = new List<int>{0,0,0,0,0};

    public int numberToGuess;
    
    public int randomAnswer;

    public int correctAnswerIndex;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var x in answerIndex) {
        Debug.Log( x.ToString());
        }
        numberToGuess = Random.Range(1, numbers.Count+1);
        Debug.Log(numberToGuess);
        correctAnswerIndex = Random.Range(0, answerIndex.Count);
        Debug.Log(correctAnswerIndex);
        for (int i = 0; i < answerIndex.Count; i++)
        {            
            if (i == correctAnswerIndex)
            {
                Debug.Log("Setting the correct number in this index");
                answerIndex[i] = numberToGuess;
                Debug.Log(numberToGuess);
            }
            else
            {
                bool tryingOriginalNumber = true;
                while (tryingOriginalNumber)
                {
                    randomAnswer = Random.Range(0,8);
                    if (!answerIndex.Contains(randomAnswer) && randomAnswer != numberToGuess)
                    {
                        Debug.Log("Found random Original number");
                        answerIndex[i] = randomAnswer;
                        Debug.Log(randomAnswer);
                        tryingOriginalNumber = false;
                    }
                }
            }
        }
    }

    public void NextQuestion()
    {
        numberToGuess = Random.Range(1, numbers.Count+1);
        numbers.RemoveAt(numberToGuess);
        if (answered == true)
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

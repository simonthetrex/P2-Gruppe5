using UnityEngine;
using System.Collections.Generic;

public class TælningLevels : MonoBehaviour
{

    public bool answered = false;

    public List<int> numbers = new List<int>{1,2,3,4,5};

    public int numberToGuess;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

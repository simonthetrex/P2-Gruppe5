using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Tårne> allTowers;
    public GameObject towerPrefab;
    public Transform towerParent;
    public Transform numberParent;
    public GameObject numberTokenPrefab;
    public GameObject nextButton; // drag your Next button here
    public GameObject EndButton;
    public TextMeshProUGUI RoundText;

    private int round = 1;
    private int totalRounds = 6;
    private int solvedCount = 0;

    void Start()
    {
        nextButton.SetActive(false);
        towerSpawn();
    }

    public void TowerSolved()
    {
        solvedCount++;
        if (solvedCount == 3)
            nextButton.SetActive(true);
    }

    public void NextRound()
    {
        solvedCount = 0;
        round++;
        nextButton.SetActive(false);
        RoundText.text = "Runde: " + round.ToString() + " af 5";

        if (round >= totalRounds)
        {
            Debug.Log("Game Over!");
            EndButton.SetActive(true);
            RoundText.text = "";
            return;
        }

        foreach (Transform child in towerParent) Destroy(child.gameObject);
        foreach (Transform child in numberParent)
            if (!child.CompareTag("NextButton"))
                Destroy(child.gameObject);

        towerSpawn();
    }

    public void towerSpawn()
    {
        List<Tårne> copy = new List<Tårne>(allTowers);
        List<Tårne> selected = new List<Tårne>();
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, copy.Count);
            selected.Add(copy[index]);
            copy.RemoveAt(index);
        }

        foreach (Tårne tower in selected)
        {
            GameObject obj = Instantiate(towerPrefab, towerParent, false);
            obj.GetComponent<TowerUI>().Setup(tower);
            obj.GetComponent<DropZone>().correctNumber = tower.klodsAmount;
        }

        List<int> numbers = new List<int>();
        foreach (Tårne tower in selected)
            numbers.Add(tower.klodsAmount);

        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }

        foreach (int n in numbers)
        {
            GameObject obj = Instantiate(numberTokenPrefab, numberParent, false);
            obj.GetComponent<DragHandler>().number = n;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
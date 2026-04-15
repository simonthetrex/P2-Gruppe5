using UnityEngine;

public class PolygonGenerator : MonoBehaviour
{
    private int correctShape;
    private int[] displayedShapes;

    public void GenerateShapes()
    {
        // Random correct shape: 3 (triangle), 4 (square), 5, or 6 sides
        correctShape = Random.Range(3, 7);

        // Build candidate list of wrong shapes (unique and not equal to correct)
        int[] candidates = new int[3];
        int candidateIndex = 0;
        for (int shape = 3; shape <= 6; shape++)
        {
            if (shape != correctShape)
            {
                candidates[candidateIndex] = shape;
                candidateIndex++;
            }
        }

        // Pick two different wrong shapes
        int firstWrongIndex = Random.Range(0, candidates.Length);
        int firstWrongShape = candidates[firstWrongIndex];

        int secondWrongIndex;
        do
        {
            secondWrongIndex = Random.Range(0, candidates.Length);
        } while (secondWrongIndex == firstWrongIndex);

        int secondWrongShape = candidates[secondWrongIndex];

        // Exactly one correct shape + two unique wrong shapes
        displayedShapes = new int[3];
        displayedShapes[0] = correctShape;
        displayedShapes[1] = firstWrongShape;
        displayedShapes[2] = secondWrongShape;

        // Shuffle display order
        for (int i = 0; i < displayedShapes.Length; i++)
        {
            int swapIndex = Random.Range(i, displayedShapes.Length);
            int temp = displayedShapes[i];
            displayedShapes[i] = displayedShapes[swapIndex];
            displayedShapes[swapIndex] = temp;
        }

        Debug.Log($"Correct shape: {correctShape} sides");
        Debug.Log($"Displayed shapes: {displayedShapes[0]}, {displayedShapes[1]}, {displayedShapes[2]}");
    }

    public bool IsCorrectShape(int index)
    {
        if (displayedShapes == null || index < 0 || index >= displayedShapes.Length)
        {
            return false;
        }

        return displayedShapes[index] == correctShape;
    }

    public int GetDisplayedShape(int index)
    {
        if (displayedShapes == null || index < 0 || index >= displayedShapes.Length)
        {
            return -1;
        }

        return displayedShapes[index];
    }

    public int GetCorrectShape()
    {
        return correctShape;
    }
}
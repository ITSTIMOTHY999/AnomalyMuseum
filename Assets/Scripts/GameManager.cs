using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 0;
    public bool[] levelHasAnomaly = new bool[9]; // index 1-8

    void Awake()
    {
        Instance = this;
        GenerateLevels();
    }

    void GenerateLevels()
    {
        // Pick 4, 5, or 6 levels to have anomalies
        int anomalyCount = Random.Range(4, 7);

        List<int> levels = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

        // Shuffle
        for (int i = levels.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = levels[i];
            levels[i] = levels[j];
            levels[j] = temp;
        }

        for (int i = 0; i < anomalyCount; i++)
        {
            levelHasAnomaly[levels[i]] = true;
        }

        Debug.Log($"Anomaly count: {anomalyCount}");
        for (int i = 1; i <= 8; i++)
            Debug.Log($"Level {i}: {(levelHasAnomaly[i] ? "ANOMALY" : "normal")}");
    }

    public void PlayerAnswered(bool saidAnomaly)
    {
        bool correct = (saidAnomaly == levelHasAnomaly[currentLevel]);

        if (correct)
        {
            Debug.Log("Correct!");
            if (currentLevel < 8)
                currentLevel++;
            else
                Debug.Log("Game complete!");
        }
        else
        {
            Debug.Log("Wrong! Going back one level.");
            if (currentLevel > 0)  // changed from 1 to 0
                currentLevel--;
        }

        UIManager.Instance.UpdateUI();
    }
}
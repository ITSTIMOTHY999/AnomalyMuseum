using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level State")]
    public int currentLevel = 0;
    public bool[] levelHasAnomaly = new bool[9]; // index 1-8

    [Header("Anomaly-capable Props")]
    [Tooltip("Drag every AnomalyObject root (e.g. Tutankhamun_Root) in here.")]
    public List<AnomalyObject> anomalyObjects = new List<AnomalyObject>();

    private int[] levelAnomalyObjectIndex = new int[9];

    void Awake()
    {
        Instance = this;
        GenerateLevels();
        ApplyLevelState();
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

        for (int i = 0; i <= 8; i++)
        {
            levelHasAnomaly[i] = false;
            levelAnomalyObjectIndex[i] = -1;
        }

        for (int i = 0; i < anomalyCount; i++)
        {
            int level = levels[i];
            levelHasAnomaly[level] = true;

            if (anomalyObjects.Count > 0)
            {
                levelAnomalyObjectIndex[level] = Random.Range(0, anomalyObjects.Count);
            }
        }

        Debug.Log($"Anomaly count: {anomalyCount}");
        for (int i = 1; i <= 8; i++)
        {
            string objName = levelAnomalyObjectIndex[i] >= 0
                ? anomalyObjects[levelAnomalyObjectIndex[i]].name
                : "none";
            Debug.Log($"Level {i}: {(levelHasAnomaly[i] ? "ANOMALY" : "normal")} (object: {objName})");
        }
    }

    void ApplyLevelState()
    {
        int chosenIndex = levelAnomalyObjectIndex[currentLevel];

        for (int i = 0; i < anomalyObjects.Count; i++)
        {
            bool shouldBeAnomaly = levelHasAnomaly[currentLevel] && (i == chosenIndex);
            anomalyObjects[i].SetAnomaly(shouldBeAnomaly);
        }
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
            if (currentLevel > 0)
                currentLevel--;
        }

        ApplyLevelState();
        UIManager.Instance.UpdateUI();
    }
}
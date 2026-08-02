using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level State")]
    public int currentLevel = 0;
    public bool[] levelHasAnomaly = new bool[9]; // index 0-8

    private int[] levelAnomalyObjectIndex = new int[9];

    [Tooltip("How many possible anomaly-capable props exist PER ROOM. Used to pick which prop index is the anomaly for a given level.")]
    public int anomalyObjectPoolSize = 3; // set this to match how many AnomalyObjects each room prefab has

    public struct LevelState
    {
        public bool hasAnomaly;
        public int anomalyObjectIndex; // which prop (by index) in the room's own list should be the anomaly
    }

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

        for (int i = 0; i <= 8; i++)
        {
            levelHasAnomaly[i] = false;
            levelAnomalyObjectIndex[i] = -1;
        }

        for (int i = 0; i < anomalyCount; i++)
        {
            int level = levels[i];
            levelHasAnomaly[level] = true;

            if (anomalyObjectPoolSize > 0)
            {
                levelAnomalyObjectIndex[level] = Random.Range(0, anomalyObjectPoolSize);
            }
        }

        Debug.Log($"Anomaly count: {anomalyCount}");
        for (int i = 1; i <= 8; i++)
        {
            Debug.Log($"Level {i}: {(levelHasAnomaly[i] ? "ANOMALY" : "normal")} (prop index: {levelAnomalyObjectIndex[i]})");
        }
    }

    // Rooms call this to find out how they should look for a given level index
    public LevelState GetLevelState(int level)
    {
        level = Mathf.Clamp(level, 0, 8);
        return new LevelState
        {
            hasAnomaly = levelHasAnomaly[level],
            anomalyObjectIndex = levelAnomalyObjectIndex[level]
        };
    }

    // Only scores the answer and updates currentLevel.
    // Map repositioning now happens in EntranceTrigger, once the player actually
    // walks into the next room, not the instant they answer.
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

        UIManager.Instance.UpdateUI();
    }
}
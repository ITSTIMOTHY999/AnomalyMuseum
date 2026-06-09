using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text levelText;
    public Text anomalyText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int level = GameManager.Instance.currentLevel;
        bool hasAnomaly = GameManager.Instance.levelHasAnomaly[level];

        levelText.text = $"Level {level}";
        anomalyText.text = hasAnomaly ? "There is an anomaly" : "There is no anomaly";
    }
}
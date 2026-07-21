using UnityEngine;

public class AnomalyObject : MonoBehaviour
{
    [SerializeField] private GameObject normalVariant;
    [SerializeField] private GameObject anomalyVariant;

    private bool isAnomaly = false;

    public System.Action<bool> OnAnomalyStateChanged;

    private void Awake()
    {
        if (normalVariant == null)
        {
            Debug.LogWarning($"AnomalyObject on '{gameObject.name}' has no Normal variant assigned.");
        }
        if (anomalyVariant == null)
        {
            Debug.LogWarning($"AnomalyObject on '{gameObject.name}' has no Anomaly variant assigned.");
        }

        ApplyState();
    }

    /// <summary>
    /// Call this from GameManager when deciding which props are anomalous
    /// for the current level.
    /// </summary>
    public void SetAnomaly(bool value)
    {
        if (isAnomaly == value) return;

        isAnomaly = value;
        ApplyState();
        OnAnomalyStateChanged?.Invoke(isAnomaly);
    }

    public bool IsAnomaly => isAnomaly;

    private void ApplyState()
    {
        if (normalVariant != null)
        {
            normalVariant.SetActive(!isAnomaly);
        }

        if (anomalyVariant != null)
        {
            anomalyVariant.SetActive(isAnomaly);
        }
    }
}
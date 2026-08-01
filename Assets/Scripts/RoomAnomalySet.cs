using System.Collections.Generic;
using UnityEngine;

public class RoomAnomalySet : MonoBehaviour
{
    [SerializeField] private List<AnomalyObject> anomalyObjects = new List<AnomalyObject>();

    public void ApplyState(GameManager.LevelState state)
    {
        for (int i = 0; i < anomalyObjects.Count; i++)
        {
            bool shouldBeAnomaly = state.hasAnomaly && i == state.anomalyObjectIndex;
            anomalyObjects[i].SetAnomaly(shouldBeAnomaly);
        }
    }
}
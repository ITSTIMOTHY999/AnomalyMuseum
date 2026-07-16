using UnityEngine;

public class AnswerTrigger : MonoBehaviour
{
    public bool thisAnswerIsAnomaly; //ON for "anomaly" object, OFF for "no anomaly"

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger hit by {other.name}, tag={other.tag}");
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerAnswered(thisAnswerIsAnomaly);
        }
    }
}
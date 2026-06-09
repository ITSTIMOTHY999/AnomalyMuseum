using UnityEngine;

public class AnswerTrigger : MonoBehaviour
{
    public bool thisAnswerIsAnomaly; //ON for "anomaly" object, OFF for "no anomaly"

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerAnswered(thisAnswerIsAnomaly);
        }
    }
}
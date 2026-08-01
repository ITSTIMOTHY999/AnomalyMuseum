using UnityEngine;

public class AnswerTrigger : MonoBehaviour
{
    public bool thisAnswerIsAnomaly; // ON for "anomaly" trigger, OFF for "no anomaly" trigger

    [SerializeField] private RoomGate myRoomGate;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[AnswerTrigger] '{gameObject.name}' hit by {other.name}, tag={other.tag}");

        if (!other.CompareTag("Player"))
        {
            Debug.Log("[AnswerTrigger] Tag mismatch, ignoring.");
            return;
        }

        if (myRoomGate == null)
        {
            Debug.LogError($"[AnswerTrigger] '{gameObject.name}' has NO myRoomGate assigned!");
            return;
        }

        Debug.Log($"[AnswerTrigger] '{gameObject.name}' checking gate '{myRoomGate.gameObject.name}', IsArmed={myRoomGate.IsArmed}");

        if (!myRoomGate.IsArmed)
        {
            Debug.Log("[AnswerTrigger] Gate not armed — ignoring.");
            return;
        }

        Debug.Log($"[AnswerTrigger] Calling PlayerAnswered({thisAnswerIsAnomaly})");
        GameManager.Instance.PlayerAnswered(thisAnswerIsAnomaly);
        myRoomGate.Disarm();
    }
}
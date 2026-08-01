using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    [SerializeField] private RoomGate myRoomGate;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[EntranceTrigger] Hit by {other.name}, tag={other.tag}");

        if (!other.CompareTag(playerTag))
        {
            Debug.Log("[EntranceTrigger] Tag mismatch, ignoring.");
            return;
        }

        if (myRoomGate == null)
        {
            Debug.LogError("[EntranceTrigger] myRoomGate is NOT assigned in the Inspector!");
            return;
        }

        myRoomGate.Arm();
        Debug.Log($"[EntranceTrigger] Armed gate on '{myRoomGate.gameObject.name}'. IsArmed = {myRoomGate.IsArmed}");
    }
}
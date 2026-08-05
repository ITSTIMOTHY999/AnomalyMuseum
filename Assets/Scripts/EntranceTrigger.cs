using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    [Tooltip("This entrance's own room root — fixed at authoring time, always the same physical room object this trigger lives inside.")]
    [SerializeField] private Transform myRoom;
    [SerializeField] private RoomGate myRoomGate;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[EntranceTrigger] '{gameObject.name}' hit by {other.name}, tag={other.tag}");

        if (!other.CompareTag(playerTag))
        {
            Debug.Log("[EntranceTrigger] Tag mismatch, ignoring.");
            return;
        }

        if (myRoom == RoomManager.Instance.roomB)
        {
            Debug.Log("[EntranceTrigger] Player walked forward into B. Repositioning map.");
            RoomManager.Instance.OnLevelChanged(true);
        }
        else if (myRoom == RoomManager.Instance.roomC)
        {
            Debug.Log("[EntranceTrigger] Player walked backward into C. Repositioning map.");
            RoomManager.Instance.OnLevelChanged(false);
        }
        else
        {
            Debug.Log("[EntranceTrigger] This is room A's own entrance (already current) — no reposition needed.");
            return;
        }

        if (myRoomGate == null)
        {
            Debug.LogError($"[EntranceTrigger] '{gameObject.name}' has NO myRoomGate assigned!");
            return;
        }

        myRoomGate.Arm();
        Debug.Log($"[EntranceTrigger] Armed gate on '{myRoomGate.gameObject.name}'. IsArmed = {myRoomGate.IsArmed}");
    }
}
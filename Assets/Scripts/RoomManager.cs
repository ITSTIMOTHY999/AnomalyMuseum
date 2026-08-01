using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [Header("Room Geometry (reassigned dynamically)")]
    public Transform roomA;
    public Transform roomB;
    public Transform roomC;

    [Header("Matching Stuff/Props (reassigned dynamically, same order as rooms above)")]
    public Transform stuffA;
    public Transform stuffB;
    public Transform stuffC;

    RoomAnomalySet setA, setB, setC;

    static readonly Vector3 frontOffsetLocal = new Vector3(18f, 0f, -52f);
    static readonly Vector3 backOffsetLocal  = new Vector3(0f, 0f, 3.7f);

    void Awake()
    {
        Instance = this;
        setA = stuffA.GetComponent<RoomAnomalySet>();
        setB = stuffB.GetComponent<RoomAnomalySet>();
        setC = stuffC.GetComponent<RoomAnomalySet>();

        setA.ApplyState(GameManager.Instance.GetLevelState(0));
        setB.ApplyState(GameManager.Instance.GetLevelState(1));
    }

    public void OnLevelChanged(bool wentForward)
    {
        Transform newRoomA, newRoomB, newRoomC;
        Transform newStuffA, newStuffB, newStuffC;

        if (wentForward)
        {
            newRoomA = roomB;  newStuffA = stuffB;
            newRoomC = roomA;  newStuffC = stuffA;
            newRoomB = roomC;  newStuffB = stuffC;
        }
        else
        {
            newRoomA = roomC;  newStuffA = stuffC;
            newRoomB = roomA;  newStuffB = stuffA;
            newRoomC = roomB;  newStuffC = stuffB;
        }

        RepositionPairAsFront(newRoomB, newStuffB, newRoomA);
        RepositionPairAsBack(newRoomC, newStuffC, newRoomA);

        int level = GameManager.Instance.currentLevel;
        newStuffB.GetComponent<RoomAnomalySet>().ApplyState(GameManager.Instance.GetLevelState(Mathf.Min(level + 1, 8)));
        newStuffC.GetComponent<RoomAnomalySet>().ApplyState(GameManager.Instance.GetLevelState(Mathf.Max(level - 1, 0)));

        // Reset gates: A is where the player now stands (no gate needed),
        // B and C are freshly recycled and must require re-entry before their answer triggers work.
        newRoomB.GetComponent<RoomGate>().Disarm();
        newRoomC.GetComponent<RoomGate>().Disarm();

        roomA = newRoomA; roomB = newRoomB; roomC = newRoomC;
        stuffA = newStuffA; stuffB = newStuffB; stuffC = newStuffC;
    }

    void RepositionPairAsFront(Transform room, Transform stuff, Transform current)
    {
        Vector3 pos = current.position + current.rotation * frontOffsetLocal;
        Quaternion rot = current.rotation;
        room.SetPositionAndRotation(pos, rot);
        stuff.SetPositionAndRotation(pos, rot);
    }

    void RepositionPairAsBack(Transform room, Transform stuff, Transform current)
    {
        Vector3 pos = current.position + current.rotation * backOffsetLocal;
        Quaternion rot = current.rotation * Quaternion.Euler(0f, 180f, 0f);
        room.SetPositionAndRotation(pos, rot);
        stuff.SetPositionAndRotation(pos, rot);
    }
}
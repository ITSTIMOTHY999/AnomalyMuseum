using UnityEngine;

public class RoomGate : MonoBehaviour
{
    public bool IsArmed { get; private set; }

    public void Arm() => IsArmed = true;
    public void Disarm() => IsArmed = false;
}
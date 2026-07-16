using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public Transform playerBody;

    [Header("Sway")]
    public float swayAmount = 1f;
    public float swaySmoothing = 2f;

    [Header("Bob")]
    public float bobSpeed = 8f;
    public float bobAmount = 0.05f;

    private float xRotation = 0f;
    private float currentSwayZ = 0f;
    private Vector2 lookInput;
    private Vector2 moveInput;
    private Vector3 initialPosition;
    private float bobTimer = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialPosition = transform.localPosition;
    }

    public void ReceiveLookInput(Vector2 input)
    {
        lookInput = input;
    }

    public void ReceiveMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    void Update()
    {
        HandleLookAndSway();
        HandleBob();
    }

    void HandleLookAndSway()
    {
        xRotation -= lookInput.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        float targetSwayZ = -lookInput.x * swayAmount;
        currentSwayZ = Mathf.Lerp(currentSwayZ, targetSwayZ, Time.deltaTime * swaySmoothing);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, currentSwayZ);

        playerBody.Rotate(Vector3.up * lookInput.x * mouseSensitivity);
    }

    void HandleBob()
    {
        float moveAmount = moveInput.magnitude;

        if (moveAmount > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float bobY = Mathf.Sin(bobTimer) * bobAmount;
            float bobX = Mathf.Cos(bobTimer * 0.5f) * bobAmount * 0.5f;

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                initialPosition + new Vector3(bobX, bobY, 0f),
                Time.deltaTime * 10f
            );
        }
        else
        {
            bobTimer = 0f;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                initialPosition,
                Time.deltaTime * 6f
            );
        }
    }
}
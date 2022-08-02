using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera overheadCamera;

    private Rigidbody playerBody;

    // Generated C# class from input action asset
    private PlayerInputAction inputAction;

    private void Awake()
    {
        // Set initial camera state
        playerCamera.enabled = true;
        overheadCamera.enabled = false;

        playerBody = GetComponent<Rigidbody>();

        inputAction = new();

        // Enable the OnFoot action map
        inputAction.OnFoot.Enable();

        // Register callback functions
        inputAction.OnFoot.Jump.performed += Jump;
        inputAction.OnFoot.ChangeCamera.performed += ChangeCamera;
    }

    private void FixedUpdate()
    {
        // Get direction
        Vector2 input = inputAction.OnFoot.Move.ReadValue<Vector2>();
        // x, y from input maps to x, z because no vertical movement
        Vector3 force = new(input.x, 0, input.y);
        playerBody.AddRelativeForce(force * moveSpeed, ForceMode.Force);

        // Rotate player and camera horizontally
        input = inputAction.OnFoot.Look.ReadValue<Vector2>();
        Quaternion xRotation = Quaternion.Euler(0, input.x * Time.fixedDeltaTime * lookSensitivity, 0);
        playerBody.MoveRotation(playerBody.rotation * xRotation);

        // Rotate camera vertically
        Quaternion yRotation = Quaternion.Euler(-input.y * Time.fixedDeltaTime * lookSensitivity, 0, 0);
        Quaternion finalRotation = yRotation * playerCamera.transform.localRotation;
        if (finalRotation.x > -0.4 && finalRotation.x < 0.4) {
            playerCamera.transform.localRotation = finalRotation;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        playerBody.AddRelativeForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void ChangeCamera(InputAction.CallbackContext context)
    {
        (playerCamera.enabled, overheadCamera.enabled) = (overheadCamera.enabled, playerCamera.enabled);
    }
}

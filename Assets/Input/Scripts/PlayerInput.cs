using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera overheadCamera;

    private Rigidbody playerBody;

    // Generated C# class from input action asset
    private PlayerInputAction inputAction;

    private void Awake()
    {
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
        float speed = 25f;
        // Get direction
        Vector2 input = inputAction.OnFoot.Move.ReadValue<Vector2>();
        // x, y from input maps to x, z because no vertical movement
        Vector3 force = new(input.x, 0, input.y);
        playerBody.AddRelativeForce(force * speed, ForceMode.Force);

        input = inputAction.OnFoot.Look.ReadValue<Vector2>();
        Vector3 rotation = new(input.y * lookSensitivity, input.x * lookSensitivity, 0);
        Quaternion delta = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        playerBody.MoveRotation(playerBody.rotation * delta);

        // Reset z rotation
        float x = playerBody.transform.rotation.x;
        float y = playerBody.transform.rotation.y;
        float z = playerBody.transform.rotation.z;
        Debug.Log($"{x},{y},{z}");
        //playerBody.transform.rotation = Quaternion.Euler(x, y, 0f);


        //if (input.x != 0)
        //{   
        //    Vector3 rotation = new(0, input.x * 50, 0);
        //    Quaternion delta = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        //    playerBody.MoveRotation(playerBody.rotation * delta);
        //}
        //if (input.y != 0)
        //{
        //    Vector3 rotation = new(input.y * 50, 0, 0);
        //    Quaternion delta = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        //    playerBody.MoveRotation(playerBody.rotation * delta);
        //}
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

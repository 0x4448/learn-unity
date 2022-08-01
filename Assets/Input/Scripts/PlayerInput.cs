using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    private Rigidbody player;

    // Generated C# class from input action asset
    private PlayerInputAction inputAction;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();

        inputAction = new();

        // Enable the OnFoot action map
        inputAction.OnFoot.Enable();

        // Add Jump() as a callback when Jump action is performed
        inputAction.OnFoot.Jump.performed += Jump;
    }

    private void FixedUpdate()
    {
        float speed = 25f;
        // Get direction
        Vector2 input = inputAction.OnFoot.Move.ReadValue<Vector2>();
        // x, y from input maps to x, z because no vertical movement
        Vector3 force = new(input.x, 0, input.y);
        player.AddForce(force * speed, ForceMode.Force);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        player.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}

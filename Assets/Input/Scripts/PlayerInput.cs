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
        player.AddRelativeForce(force * speed, ForceMode.Force);

        input = inputAction.OnFoot.Look.ReadValue<Vector2>();
        if (input.x != 0)
        {   
            Vector3 rotation = new(0, input.x * 50, 0);
            Quaternion delta = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            player.MoveRotation(player.rotation * delta);
        }
        if (input.y != 0)
        {
            Vector3 rotation = new(input.y * 50, 0, 0);
            Quaternion delta = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            player.MoveRotation(player.rotation * delta);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        player.AddRelativeForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}

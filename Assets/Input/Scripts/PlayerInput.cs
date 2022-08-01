using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    private Rigidbody player;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();

        // Generated C# class from input action asset
        PlayerInputAction inputAction = new();
        inputAction.OnFoot.Enable();
        inputAction.OnFoot.Jump.performed += Jump;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        player.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}

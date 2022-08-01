using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    private Rigidbody player;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}

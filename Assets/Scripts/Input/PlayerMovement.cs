using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private PlayerControls Input;
    private CharacterController controller;

    public float Speed = 2.0f;
    private Vector3 moveInput = Vector3.zero;
    public float x { get; private set; }
    public float z { get; private set; }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveInput.x = x;
        moveInput.z = z;
        controller.Move(moveInput * Time.deltaTime * Speed);
    }
    
    void OnMovement(InputValue value)
    {
        x = value.Get<Vector2>().x;
        z = value.Get<Vector2>().y;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{
    //private PlayerControls InputValue;

    [Header("Movement Settings")]
    private CharacterController controller;
    private PlayerControls ctrl;

    public float Speed = 2.0f;
    private float turnSpeed;
    private float turnTime = 0.1f;
    public Vector2 moveInput = Vector2.zero;

    public float jumpHeight = 3.0f;
    public bool isJumping = false;

    [Header("Camera Settings")]
    public Transform camFollow;
    public Transform camAim;
    public Camera camMain;

    [Header("World interactive settings")]
    public bool fire;
    public bool isInteracting = false;

    public delegate void IPlayerInteract();
    public delegate void IPlayerJump();

    public static event IPlayerInteract OnInteract;
    public static event IPlayerJump OnJump;

    [Header("Animator")]
    public Animator Animator;

    private void Awake()
    {
        ctrl = new PlayerControls();


        // += ctx => Function;
        // Create Function()
        ctrl.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        ctrl.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        ctrl.Player.Interaction.performed += ctx => OnPlayerInteraction();
        ctrl.Player.Interaction.canceled += ctx => isInteracting = false;

        ctrl.Player.Jump.performed += ctx => OnPlayerJump();
        ctrl.Player.Jump.canceled += ctx => isJumping = false;
    }



    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        ctrl.Enable();
    }

    private void OnDisable()
    {
        ctrl.Disable();    
    }


    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0.0f, moveInput.y);
        move = camMain.transform.forward * move.z + camMain.transform.right * move.x;
        move.y = 0.0f;
        move.Normalize();
        controller.Move(move * Speed * Time.deltaTime);

        if (move.magnitude >= 0.01f)
        {
            Animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + camFollow.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDirection.y = 0;
        }
        else
        {
            Animator.SetBool("isRunning", false);
        }
    }


    public void OnPlayerInteraction()
    {
        isInteracting = true;
        OnInteract?.Invoke();
        print("The player is trying to interact.");
    }

    public void OnPlayerJump()
    {
        isJumping = true;
        OnJump?.Invoke();
        print("Is jumping!");


        Vector3 jumpDir = new Vector3(0.0f, jumpHeight, 0.0f);

        controller.Move(jumpDir * Time.deltaTime);
    }
    //private void Movement_started(InputAction.CallbackContext context)
    //{
        //moveInput = context.ReadValue<Vector2>();
    //}
}

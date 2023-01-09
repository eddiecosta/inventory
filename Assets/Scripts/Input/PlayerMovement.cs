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
    public Vector3 Velocity = Vector3.zero;
    public float yVelocity;

    public float jumpHeight = 1.0f;
    public float jumpSpeed = 2.0f;
    public float Gravity = 1.0f;
    public float idleGravity = -0.1f;

    [Header("Camera Settings")]
    public Transform camFollow;
    public Transform camAim;
    public Camera camMain;

    [Header("World interactive settings")]
    public bool fire;
    public bool isInteracting = false;
    public bool isJumping = false;

    //public delegate void IPlayerInteract();
    //public static event IPlayerInteract OnInteract;

    [Header("Animator")]
    public Animator Animator;

    private void Awake()
    {
        ctrl = new PlayerControls();


        // += ctx => Function;
        // Create Function()
        ctrl.Player.Movement.performed += _ => moveInput = _.ReadValue<Vector2>();
        ctrl.Player.Movement.canceled += _ => moveInput = Vector2.zero;

        ctrl.Player.Interaction.performed += _ => PlayerActions.OnPressE();
        ctrl.Player.Interaction.canceled += _ => isInteracting = false;

        ctrl.Player.Jump.performed += _ => PlayerActions.OnJump();
        ctrl.Player.Jump.canceled += _ => isJumping = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        ctrl.Enable();
        PlayerActions.OnPressE += OnInteract;
        PlayerActions.OnJump += CanJump;
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

        move.y = yVelocity;

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


        if (isJumping == true)
        {
            yVelocity = jumpHeight;            
        }
        else
        {
            isJumping = false;
            yVelocity -= 0.1f;
            yVelocity = Mathf.Clamp(-0.005f, -0.009f, 0.0f);
        }
        //if (isInteracting == true)
        //{
        //    if (OnInteract != null)
        //        OnInteract();
        //    print("is interacting");
        //}
    }

    private void OnInteract()
    {
        print("Is trying to interact something.");
        
        isInteracting = true;
    }

    private void CanJump()
    {
        print("Jump!");
        isJumping = true;

        // Jump function
        //Vector3 jumpDir = new Vector3(this.transform.position.x, jumpHeight, this.transform.position.z);
        //controller.Move(jumpDir * Time.deltaTime);
    }

    //private void Movement_started(InputAction.CallbackContext context)
    //{
        //moveInput = context.ReadValue<Vector2>();
    //}
}

using UnityEngine;
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

    private Vector2 moveInput = Vector2.zero;

    [Header("Camera Settings")]
    public Transform camFollow;
    public Transform camAim;
    public Camera camMain;

    [Header("World interactive settings")]
    public bool fire;
    public bool isInteract { get; private set; }

    [Header("Animator")]
    public Animator Animator;

    private void Awake()
    {
        ctrl = new PlayerControls();
    }

    private void OnEnable()
    {
        ctrl.Enable();
    }

    private void OnDisable()
    {
        ctrl.Disable();    
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        ctrl.Player.Movement.started += Movement_started;
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

    //void OnMovement(InputValue value)
    //public void OnMovement(InputAction.CallbackContext context)
    //{
    //    moveInput = context.ReadValue<Vector2>();
    //}

    private void Movement_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

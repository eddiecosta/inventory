using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _controller;
    MousePOS mousePOS;

    public bool isCheckAim;
    private bool isJump;
    private bool isGrounded;
    private float h;
    private float v;

    private float directionY;
    public float jumpSpeed = 3f;
    public float gravity = 10f;
    public float moveSpeed = 5f;

    private float _rotationDuration = 0.15f;
    private float _rotationVel;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        mousePOS = GetComponent<MousePOS>();
        isCheckAim = mousePOS.isAiming;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // PlayerMovement action
        MovementInputHandler();

        //isCheckAim = mousePOS.isAiming;

        Vector3 moveInput = new Vector3(h, 0f, v).normalized;

        if (isCheckAim == false && moveInput.magnitude >= 0.1f)
        {
            var angle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
            float _targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _rotationVel, _rotationDuration);
            transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);
        }

        // Jump Input

        directionY -= gravity * Time.deltaTime;
        moveInput.y = directionY;
        if (moveInput.y < -9.0f) moveInput.y = -9.0f;

        if (Input.GetButtonDown("Jump")/* && isGrounded == true*/)
        {
            Jump();
        }

        _controller.Move(moveInput * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        isJump = true;
        directionY = jumpSpeed;
    }

    private void MovementInputHandler()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        var jumpAction = Input.GetButton("Jump");
    }
}

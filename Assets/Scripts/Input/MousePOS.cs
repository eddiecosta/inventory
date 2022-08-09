using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePOS : MonoBehaviour
{
    private CharacterController Controller;

    [SerializeField] private float _rotationDuration = 0.15f;
    [SerializeField] private float _rotationVel;

    private Camera cam = null;
    //[SerializeField] GameObject Target = null;
    public GameObject FirePoint;
    public bool isAiming = false;

    private void Start()
    {
        cam = Camera.main;
        isAiming = false;
    }

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.None;
            TargetMousePos();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            isAiming = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void TargetMousePos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var myPos = transform.position;
            myPos.y = FirePoint.transform.position.y;

            Vector3 LookTarget = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            var targetPos = LookTarget;
            targetPos.y = FirePoint.transform.position.y;

            Vector3 toOther = (myPos - targetPos).normalized;

            var angle = Mathf.Atan2(toOther.x, toOther.z) * Mathf.Rad2Deg + 180;
            float _targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _rotationVel, _rotationDuration);
            transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);
        }
    }
}
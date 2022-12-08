using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook camFollow;

    public CinemachineVirtualCamera camAim;

    private CinemachineFreeLook freeLook;

    //bool isAiming = false;
    //float timer = 5.0f;

    private void Awake()
    {
        freeLook = camFollow.GetComponent<CinemachineFreeLook>();
        //isAiming = false;
        if (!camFollow) print("No follow camera.");
        else if (!camAim) print("No aim camera.");
    }

    private void Start()
    {
        //isAiming = false;
    }
    //private void Update()
    //{
    //    if (Keyboard.current.fKey.wasPressedThisFrame && camFollow.m_Priority > 10 && timer > 0)
    //    {
    //        print("Switched camera!");
    //        isAiming = true;
    //        camFollow.m_Priority = 9;
    //        timer -= Time.deltaTime;
    //    }

    //    if (Keyboard.current.fKey.wasPressedThisFrame && camFollow.m_Priority < 10 && isAiming == true && timer == 5.0f)
    //    {
    //        print("Switch camera");
    //        isAiming = false;
    //        camFollow.m_Priority = 11;
    //    }

    //    if (timer < 0.0f)
    //        timer = 0;
    //if (Keyboard.current.gKey.wasPressedThisFrame)
    //{
    //    print(freeLook.m_Priority);
    //    freeLook.m_Priority = 1;
    //    print(freeLook.m_Priority);
    //}
    //}
}

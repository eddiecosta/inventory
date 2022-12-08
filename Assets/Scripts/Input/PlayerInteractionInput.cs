using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionInput : MonoBehaviour
{
    private PlayerControls playerControls;
    public bool isInteracting = false;

    void Start()
    {
        playerControls = new PlayerControls();
    }

    void Update()
    {

    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        print("Hello!");
    }


}

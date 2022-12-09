using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ChestActivate : MonoBehaviour
{
    private BoxCollider col;

    public GameObject physicalChest;
    public GameObject Highlight;
    private Renderer physicalChestRenderer;

    public Material defaultChestShader;
    public Material highlightedChestShader;
    public bool isActive;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        physicalChestRenderer = physicalChest.GetComponent<Renderer>();
        col.isTrigger = true;
        isActive = false;
        Highlight.SetActive(false);

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InventoryHolder>())
        {
            PlayerMovement.OnInteract += ChestInteracting;
            isActive = true;
            physicalChestRenderer.material = highlightedChestShader;
            Highlight.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InventoryHolder>())
        {
            PlayerMovement.OnInteract -= ChestInteracting;
            isActive = false;
            physicalChestRenderer.material = defaultChestShader;
            Highlight.SetActive(false);
        }
    }

    private void ChestInteracting()
    {
        if (isActive)
            print("Chest is being opened");
    }
}

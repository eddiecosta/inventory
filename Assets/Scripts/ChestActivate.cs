using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ChestActivate : MonoBehaviour
{
    private BoxCollider col;

    public GameObject physicalChest;
    //public GameObject Highlight;
    private Renderer physicalChestRenderer;
    private InventoryHolder thisInventory;

    public Material defaultChestShader;
    public Material highlightedChestShader;
    public bool isActive;


    private InventoryHolder selectedHolder;
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        physicalChestRenderer = physicalChest.GetComponent<Renderer>();
        col.isTrigger = true;
        isActive = false;
        //Highlight.SetActive(false);

        thisInventory = GetComponent<InventoryHolder>();

    }

    private void OnEnable()
    {
        PlayerActions.OnPressE += ChestInteracting;
    }

    private void OnDisable()
    {
        PlayerActions.OnPressE -= ChestInteracting;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InventoryHolder>())
        {
            isActive = true;
            physicalChestRenderer.material = highlightedChestShader;
            //Highlight.SetActive(true);

            selectedHolder = other.GetComponent<InventoryHolder>();


            //if (other.GetComponent<PlayerMovement>().isInteracting == true)
            //{
            //    other.GetComponent<InventoryHolder>().InventorySystem.AddToInventory(thisInventory.InventorySystem.InventorySlots[0].ItemData, 1);
            //}
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InventoryHolder>())
        {
            isActive = false;
            physicalChestRenderer.material = defaultChestShader;
            //Highlight.SetActive(false);
        }
    }

    private void ChestInteracting()
    {
        if (isActive)
        {
            print("Chest is being opened");
            selectedHolder.InventorySystem.AddToInventory(thisInventory.InventorySystem.InventorySlots[0].ItemData, 1);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GenerateChestInventory :MonoBehaviour
{
    //public List<InventoryItemData> ItemPooling = new List<InventoryItemData>();
    //public CreateItemList ThisChestInventory;
    public List<InventoryItemData> ListOfItemData;
    
    private InventoryItemData itemData;
    private InventoryHolder GetHolder;
    //private bool withinReach = false;
    //public bool isInteracting = false;
    //public int ChestInventorySize => ItemPooling.Count;

    private PlayerControls ctrl;

    public void Awake()
    {
        ListOfItemData = GameObject.Find("GameManager").GetComponent<CreateItemList>().ItemList;
        
        GetHolder = this.gameObject.GetComponent<InventoryHolder>();

        //withinReach = this.gameObject.GetComponent<ChestActivate>().isActive;

        //ctrl.Player.Interaction.performed += ctx => isInteracting = true;
        //ctrl.Player.Interaction.canceled += ctx => isInteracting = false;
    }

    private void Start()
    {
        for (int i = 0; i < GetHolder.InventorySystem.InventorySize; i++)
        {
            GetHolder.InventorySystem.AddToInventory(ListOfItemData[Random.Range(0, ListOfItemData.Count)], Random.Range(1, 5));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChestInventory :MonoBehaviour
{
    //public List<InventoryItemData> ItemPooling = new List<InventoryItemData>();
    //public CreateItemList ThisChestInventory;
    public List<InventoryItemData> ListOfItemData;
    
    private InventoryItemData itemData;
    public InventoryHolder GetHolder;
    //public int ChestInventorySize => ItemPooling.Count;

    public void Awake()
    {
        ListOfItemData = GameObject.Find("GameManager").GetComponent<CreateItemList>().ItemList;
        
        GetHolder = this.gameObject.GetComponent<InventoryHolder>();
    }

    private void Start()
    {
        for (int i = 0; i < GetHolder.InventorySystem.InventorySize; i++)
        {
            GetHolder.InventorySystem.AddToInventory(ListOfItemData[Random.Range(0, ListOfItemData.Count)], Random.Range(1, 5));
        }
    }
}
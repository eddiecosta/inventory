using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;

    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {
        // placeholder
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);
    
    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) // Slot value 
            {
                slot.Key.UpdateUISlot(updatedSlot); // Slot key - UI representation of the valuze
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {

        // Clicked slot HAS an item and mouse DOESNT item, pick up item

        if (clickedUISlot.AssignedInventorySlot.Data != null && mouseInventoryItem.AssignedInventorySlot.Data == null)
        {
            // Shift  + Click -> split stack

            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();

        }

        // Clicked slot doesn't have and item and mouse does, place item in empty slot

        // Both slots have item, decide what to do
        // Both items are the same, combine
        // Is slot stack size + mouse stack size < max stack size, then take fom mouse
        // If different items, swap
    }
}

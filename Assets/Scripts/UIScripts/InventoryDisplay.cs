using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;



        if (clickedUISlot.AssignedInventorySlot.ItemData != null 
            && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            // Shift  + Click -> split stack
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) // split stack
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot); ;
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }

        }

        // Clicked slot doesn't have and item and mouse does, place item in empty slot
        if (clickedUISlot.AssignedInventorySlot.ItemData == null 
            && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }

        // Both slots have item, decide what to do
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;

            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
            }

            else if(isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickedUISlot); // Stack is full so swap items
                else // Slot is not at max, take what is needed from mouse inventory
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
            }
            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
            }
        }
        // Both items are the same, combine
        // Is slot stack size + mouse stack size < max stack size, then take fom mouse
        // If different items, swap
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}

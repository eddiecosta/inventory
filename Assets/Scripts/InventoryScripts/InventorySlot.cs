using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData; // Reference to the data
    [SerializeField] private int stackSize; // Current stack size

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount) // Constructor to make a occupied inventory slot
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() // Constructor to make an empty inventory slot
    {
        ClearSlot();
    }

    public void ClearSlot() // clears slots
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.ItemData)
        {
            AddToStack(invSlot.stackSize);
        } 
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) // Would there be enough room in the stack
    {
        amountRemaining = itemData.MaxStackSize - stackSize;

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (itemData != null || stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (StackSize <= 1) // Is there enough to actually split. If not, return false.
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(StackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(ItemData, halfStack);
        return true;
    }
}

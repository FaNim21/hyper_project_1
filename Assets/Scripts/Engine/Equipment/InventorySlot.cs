using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemData itemData; // reference to data of our item
    [SerializeField] private int stackSize; // to store the current stack of item

    public ItemData ItemData => itemData; // getter
    public int StackSize => stackSize; // getter

    public InventorySlot(ItemData source, int amount) // constructor to non-empty slot
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() // constructor to an empty slot
    {
        ClearSlot();
    }

    public void ClearSlot() // method that clears the slot
    {
        itemData = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(ItemData data, int amount) // method that updates the slot with entire data of an item and its amount
    {
        itemData = data;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // method that checks if theres anough room (enough amount to maxstacksize) to add

    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) 
            return true; // if current stacksize plus amount we want to add is <= max of stacksize of the item, then there is enough room and we can add to this one stack
        else 
            return false; // else we cannot
    }

    public void AddToStack(int amount)
    {
        stackSize = stackSize + amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize = stackSize - amount;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.StackSize);
        else
        {
            itemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.StackSize);
        }
    }

}

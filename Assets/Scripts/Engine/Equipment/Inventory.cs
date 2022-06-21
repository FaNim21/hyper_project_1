using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class Inventory
{
    [SerializeField] private List<InventorySlot> inventorySlots; // list to store amount of slots
  
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged; // event occurs at slot change

    public Inventory(int size) // constructor to set the amount of slots
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }
    public bool AddToInventory(ItemData itemToAdd, int amountToAdd) // method that adds item to our inventory
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // checking if item already exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        if (HasFreeSlot(out InventorySlot freeSlot)) // it gets the first available slot 
        {
            if(freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }
        return false;
    }
    public bool ContainsItem(ItemData itemToAdd, out List<InventorySlot> invSlot) // method is checking whether any of our slots have item to add in them or not
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList(); // if yes, get the list of them
        Utils.Log(invSlot.Count.ToString()); 
        return invSlot != null; // if yes, return true, else false
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null); // getting the first free slot
        return freeSlot != null;
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData
{
    public ItemData itemData;
    public int quantity;

    public SlotData(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}

public class Inventory : MonoBehaviour
{
    public List<SlotData> items = new();
    public List<InventorySlot> slots = new(); //slotsy to powinny bys sloty rzeczywistego ekwipunku a nie quick slotow ktore powinny miec swoja baze slotow albo byc oddzielnie zczytywane

    public int InventorySize => items.Count;
    public int maxSize;


    public bool AddToInventory(ItemData itemToAdd, int amountToAdd) // method that adds item to our inventory
    {
        foreach (var slot in slots)
        {
            if(slot.data.itemData == itemToAdd && slot.data.quantity + amountToAdd <= itemToAdd.MaxStackSize)
            {
                slot.AddToStack(amountToAdd);
                return true;
            }
        }

        if (items.Count >= maxSize) return false;

        var newData = new SlotData(itemToAdd, amountToAdd);
        items.Add(newData);

        for (int i = 0; i < slots.Count; i++)
        {
            var currentSlot = slots[i];
            if (currentSlot.data.itemData == null)
            {
                currentSlot.UpdateUISlot(newData);
                return true;
            }
        }

        return false;

        /*if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // checking if item already exists in inventory
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
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }*/
        //return false;
    }

    /*public bool ContainsItem(ItemData itemToAdd, out List<InventorySlot> invSlot) // method is checking whether any of our slots have item to add in them or not
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList(); // if yes, get the list of them
        Utils.Log(invSlot.Count.ToString()); 
        return invSlot != null; // if yes, return true, else false
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null); // getting the first free slot
        return freeSlot != null;
    }*/
}

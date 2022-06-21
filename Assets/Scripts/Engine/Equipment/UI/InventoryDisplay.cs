using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    public Inventory inventory;
    public List<InventorySlot> slots = new(); // pairing up the UI slots with inventory system slots

    public int displaySize;


    public virtual void Start()
    {
        //AssignSlot();
    }

    //public abstract void AssignSlot();

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        /*slotDictionary.TryGetValue(updatedSlot, out var slotUI);
        slotUI.UpdateUISlot();*/

        /*foreach (var slot in slots)
            if (slot == updatedSlot)
                slot.UpdateUISlot(updatedSlot);*/
    }
    public void SlotClicked(InventorySlot clickedSlot)
    {
        Utils.Log("Slot clicked");
        // add splitting the stacks(maybe by shift), combining stacks, swapping items, clicking and draggin item from the slot
    }
}

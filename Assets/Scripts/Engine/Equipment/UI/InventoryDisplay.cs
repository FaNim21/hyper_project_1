using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected Inventory inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary; // pairing up the UI slots with inventory system slots

    public Inventory InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(Inventory invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }
    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Utils.Log("Slot clicked");
        // add splitting the stacks(maybe by shift), combining stacks, swapping items, clicking and draggin item from the slot
    }
}

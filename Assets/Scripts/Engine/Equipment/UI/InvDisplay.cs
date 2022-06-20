using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InvDisplay : MonoBehaviour
{
    [SerializeField] MouseItemObject mouseInventoryItem;
    protected Inventory inventorySystem;
    protected Dictionary<InvSlot_UI, InventorySlot> slotDictionary;

    public Inventory InventorySystem => inventorySystem;
    public Dictionary<InvSlot_UI, InventorySlot> SlotDictionary => slotDictionary;

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

    public void SlotClicked(InvSlot_UI clickedSlot)
    {
        Utils.Log("Wcisnieto Slot, probka");
    }
}

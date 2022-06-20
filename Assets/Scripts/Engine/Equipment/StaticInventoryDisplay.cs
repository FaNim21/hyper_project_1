using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] private InventorySlot_UI[] slots;
    protected override void Start()
    {
        base.Start();

        if (inventoryPlayer != null)
        {
            inventorySystem = inventoryPlayer.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Utils.LogWarning($"No inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(Inventory invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (slots.Length != inventorySystem.InventorySize) Utils.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}

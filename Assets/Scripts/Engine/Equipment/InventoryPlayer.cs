using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected Inventory inventorySystem;

    public Inventory InventorySystem => inventorySystem;

    public static UnityAction<Inventory> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventorySystem = new Inventory(inventorySize);
    }
}

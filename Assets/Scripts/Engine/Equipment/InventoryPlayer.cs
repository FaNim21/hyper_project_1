using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// class to integrate player with inventory system, 
/// player will have 2 inventory systems - one for quickbar, one for whole equipment
/// </summary>
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

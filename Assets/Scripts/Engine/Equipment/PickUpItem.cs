using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public ItemData ItemData;

    private BoxCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.isTrigger = true;
        myCollider.edgeRadius = PickUpRadius;
    }
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.transform.GetComponent<InventoryPlayer>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }*/
}

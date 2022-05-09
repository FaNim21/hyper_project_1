using UnityEngine;

public class PickingUpItems : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log(item.name + " picked");
            bool isPickedUp = Inventory.instance.Add(item);
            if (isPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}

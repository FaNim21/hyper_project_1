using UnityEngine;

public class PickingUpItems : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player") // Gdy nastepuje kolizja z graczem, ktory jest otagowany jako "Player"
        {
            Debug.Log(item.name + " picked");
            bool isPickedUp = Inventory.instance.Add(item); // Dodaje dotkniety przedmiot do ekwipunku (w kodzie)
            if (isPickedUp)
            {
                Destroy(gameObject); // Przedmiot, ktorego dotyka gracz jest niszczony (znika)
            }
        }
    }
}

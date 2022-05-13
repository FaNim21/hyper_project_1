using UnityEngine;

public class Item : MonoBehaviour
{
    public static Item item { get; private set; }

    public string name = "New Item";
    public string desc = "New description";
    public Sprite icon = null;
    public float worth;
    public float weight;
    public int stacksize;
    public ItemType type;

    public enum ItemType
    {
        item,
        weapon,
        armor,
        potion,
        food
    }
}

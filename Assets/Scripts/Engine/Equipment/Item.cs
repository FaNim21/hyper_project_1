using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    #region Singleton

    public static Item instance;

    public void Awake()
    {
        instance = this;
    }

    #endregion

    public string name = "New Item";
    public string desc = "New description";
    public Sprite icon = null;
    public float worth;
    public float weight;
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

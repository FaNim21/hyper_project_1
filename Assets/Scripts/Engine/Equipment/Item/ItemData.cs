using UnityEngine;

/// <summary>
///  ItemData as scriptable object to store the information for the whole item
/// </summary>

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Items/Equipment")]
public class ItemData: ScriptableObject
{
    public int ID;
    public string Name;
    // bigger window to text
    [TextArea(4,4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public int worth;
    public int weight;
}
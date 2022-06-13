using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Items/Equipment")]
public class ItemData: ScriptableObject
{
    public int ID;
    public string Name;
    [TextArea(4,4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
}
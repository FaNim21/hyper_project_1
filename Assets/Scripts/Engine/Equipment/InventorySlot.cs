using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public InventoryDisplay ParentDisplay { get; private set; }

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textItemCount;
    [SerializeField] private Button button;

    public SlotData data; //reference to data of our item

    public ItemData ItemData { get { return data.itemData; } set { data.itemData = value; } } // getter & setter
    public int StackSize { get { return data.quantity; } set { data.quantity = value; } } // getter & setter


    private void Awake()
    {
        ClearData();

        button.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void ClearData() // method that clears the slot
    {
        ItemData = null;
        StackSize = 0;
    }
    public void UpdateInventorySlot(ItemData data, int amount) // method that updates the slot with entire data of an item and its amount
    {
        ItemData = data;
        StackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // method that checks if theres anough room (enough amount to maxstacksize) to add
    {
        amountRemaining = ItemData.MaxStackSize - StackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }
    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        return ItemData == null || ItemData != null && StackSize + amountToAdd <= ItemData.MaxStackSize;
        /*if (ItemData == null || ItemData != null && StackSize + amountToAdd <= ItemData.MaxStackSize)
            return true; // if current stacksize plus amount we want to add is <= max of stacksize of the item, then there is enough room and we can add to this one stack
        else
            return false; // else we cannot*/
    }

    public void AddToStack(int amount) => StackSize += amount;
    public void RemoveFromStack(int amount) => StackSize -= amount;

    public void AssignItem(InventorySlot invSlot)
    {
        if (ItemData == invSlot.ItemData) AddToStack(invSlot.StackSize);
        else
        {
            ItemData = invSlot.ItemData;
            StackSize = 0;
            AddToStack(invSlot.StackSize);
        }
    }

    /*public void Init(InventorySlot slot)
    {
        UpdateUISlot(slot);
    }*/

    public void UpdateUISlot(SlotData newData)
    {
        /*if (slot.ItemData != null)
        {
            image.sprite = slot.ItemData.Icon;
            image.color = Color.white;

            if (slot.StackSize > 1) textItemCount.text = slot.StackSize.ToString();
            else textItemCount.text = "";
        }
        else
        {
            ClearData();
        }*/

        data = newData;

        image.sprite = newData.itemData.Icon;
        image.color = Color.white;

        if (StackSize > 1) textItemCount.text = StackSize.ToString();
        else textItemCount.text = "";
    }

    public void ClearSlot()
    {
        ClearData();
        image.sprite = null;
        image.color = Color.clear;
        textItemCount.text = "";
    }

    public void OnUISlotClick() => ParentDisplay.SlotClicked(this);
}

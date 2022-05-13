using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance { get; private set; }

    private int space = 50;
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("There is not enough space in equipment");
            return false;
        }

        items.Add(item);
        return true;

    }

    public void Awake()
    {
        instance = this;
    }

    /* public void Drop(Item item)
     {
         items.Remove(item);
     }*/
}

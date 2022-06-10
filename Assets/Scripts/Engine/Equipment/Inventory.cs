using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int space = 50; // Pojemnoœæ naszego ekwipunku
    public List<ItemData> items = new List<ItemData>(); // Lista itemow znajdujacych sie aktualnie w naszym ekwipunku

    public bool Add(ItemData item) // Dodawanie itemow do eq
    {
        if (items.Count >= space) // Jesli ilosc itemow jest wieksza niz ilosc miejsca w eq to item nie zostanie podniesiony
        {
            Debug.Log("There is not enough space in equipment");
            return false;
        }
        //dodanie itemu do slotu 
        items.Add(item);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) // Wykorzystanie interfejsu - upuszczenie przedmiotu w slocie
    {
        if (eventData.pointerDrag == null) return;
        Debug.Log("OnDrop");
        
        DragAndDrop dragAndDrop = eventData.pointerDrag.GetComponent<DragAndDrop>();
        dragAndDrop.transform.position = transform.position; // wstawia przedmiot na œrodek slotu
    }
}

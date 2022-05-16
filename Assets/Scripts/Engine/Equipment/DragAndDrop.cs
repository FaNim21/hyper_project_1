using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransform przetrzymuje i manipuluje wartosciami pozycji, rozmiaru i zakotwiczenia dla prostokatu
    }

    public void OnBeginDrag(PointerEventData eventData) // Interface wzywany zanim rozpocznie siê przesuwanie przedmiotu
    {
        Debug.Log("DragBegin");
    }

    public void OnDrag(PointerEventData eventData) // Interface wzywany za ka¿dym razem gdy przedmiot jest przesuwany (w trakcie ruszania kursorem)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) // Interface wzywany gdy koñczymy przesuwaæ przedmiot
    {
        Debug.Log("DragEnd");
    }

    public void OnPointerDown(PointerEventData eventData) // Interface wzywany gdy wykrywa klikniêcie myszki na przedmiot
    {
        Debug.Log("Point");
    }
}
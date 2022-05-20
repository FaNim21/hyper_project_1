using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransform przetrzymuje i manipuluje wartosciami pozycji, rozmiaru i zakotwiczenia dla prostokatu
        canvasGroup = GetComponent<CanvasGroup>(); // Canvasgroup - atrybut dla itemów - pozwala na utworzenie z niego obiektu ,,interaktywnego'', pozwala na zmiane przezroczystosci i zmiane atrybutu ,,block raycast''
    }

    public void OnBeginDrag(PointerEventData eventData) // Interface wzywany zanim rozpocznie sie przesuwanie przedmiotu
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f; // zwiększa poziom przezroczystości, gdy łapiemy za przedmiot
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData) // Interface wzywany za kazdym razem gdy przedmiot jest przesuwany (w trakcie ruszania kursorem)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // przesuwanie itemem, skalowane wraz z canvasem
    }

    public void OnEndDrag(PointerEventData eventData) // Interface wzywany gdy konczymy przesuwac przedmiot
    {
        Debug.Log("OnDragEnd");
        canvasGroup.alpha = 1f; // na koniec łapania powrót do normalnego poziomu przezroczystosci
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData) // Interface wzywany gdy wykrywa klikniecie myszki na przedmiot
    {
        Debug.Log("OnPointer");
    }
    

}
using UnityEngine;

public class Dragging : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOffset;

    private void Awake()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
        Debug.Log("OnMouseDown");
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}

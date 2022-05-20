using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 dragOffset;
    private Camera cam;
    [SerializeField] private float drag_speed = 10;


    void Awake() {
        cam = Camera.main;
    }

    void OnMouseDown() {
        dragOffset = transform.position - GetMousePos();
    }
    
    void OnMouseDrag() {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, drag_speed * Time.deltaTime);
    }

    Vector3 GetMousePos() {

        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;

    }




}

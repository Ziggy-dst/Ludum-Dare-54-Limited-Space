using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging;

    void Start()
    {

    }

    void Update()
    {
        Drag();
    }

    private void Drag()
    {
        if (isDragging)
        {
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector2)offset;
            transform.position = new Vector2(newPosition.x, newPosition.y);
        }
    }

    private void OnMouseDown()
    {
        print("down");
        isDragging = true;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        print("up");
        isDragging = false;
    }
}

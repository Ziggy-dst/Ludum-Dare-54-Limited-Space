using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public Vector2 objectSize;
    public float cellSize = 1;

    private Vector2 offset;
    private bool isDragging = false;
    private bool canDrop = true;

    // position when start to drag
    private Vector2 originalPosition;
    // position after drag
    private Vector2 newPosition;

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
            newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector2)offset;
            transform.position = new Vector2(newPosition.x, newPosition.y);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        originalPosition = transform.position;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        // TODO: check if outside of the viewport
        if (canDrop) transform.position = Snap(newPosition, cellSize);
        else transform.position = originalPosition;
        isDragging = false;
    }

    private Vector2 Snap(Vector2 position, float cellSize)
    {
        float adjustedX = Mathf.Round(position.x - objectSize.x / 2f);
        float adjustedY = Mathf.Round(position.y - objectSize.y / 2f);

        float x = Mathf.Round(adjustedX / cellSize) * cellSize + objectSize.x / 2f;
        float y = Mathf.Round(adjustedY / cellSize) * cellSize + objectSize.y / 2f;

        return new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("PlayableUI")) canDrop = false;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("PlayableUI")) canDrop = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public Vector2 objectSize;
    public float cellSize = 1;
    protected Vector2 offset;

    [HideInInspector]
    public bool isDragging = false;
    [HideInInspector]
    public bool canDrop = true;

    // position when start to drag
    protected Vector2 originalPosition;
    // position after drag
    protected Vector2 newPosition;

    void Update()
    {
        CalculateNewPosition();
        Drag();
    }

    private void CalculateNewPosition()
    {
        newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }

    protected virtual void Drag()
    {
        if (isDragging) transform.position = new Vector2(newPosition.x, newPosition.y);
    }

    protected virtual void OnMouseDown()
    {
        isDragging = true;
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 999;
    }

    protected virtual void OnMouseUp()
    {
        isDragging = false;
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    protected Vector2 Snap(Vector2 position, float cellSize)
    {
        float adjustedX = Mathf.Round(position.x - objectSize.x / 2f);
        float adjustedY = Mathf.Round(position.y - objectSize.y / 2f);

        float x = Mathf.Round(adjustedX / cellSize) * cellSize + objectSize.x / 2f;
        float y = Mathf.Round(adjustedY / cellSize) * cellSize + objectSize.y / 2f;

        return new Vector2(x, y);
    }
}

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
    public bool inViewport = false;
    [HideInInspector]
    public bool canDrop = true;

    // position when start to drag
    protected Vector2 originalPosition;
    // position after drag
    protected Vector2 newPosition;

    public IntersectionCheck intersectionCheck;

    protected virtual void Update()
    {
        CalculateNewPosition();
        Drag();
    }

    private void CalculateNewPosition()
    {
        newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }

    protected void Drag()
    {
        if (isDragging) transform.position = new Vector2(newPosition.x, newPosition.y);
    }

    protected virtual void OnMouseDown()
    {
        isDragging = true;
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 999;

        originalPosition = transform.position;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void OnMouseUp()
    {
        isDragging = false;
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    protected virtual Vector2 Snap(Vector2 position)
    {
        float adjustedX = position.x - objectSize.x / 2f - 0.5f;
        float adjustedY = position.y - objectSize.y / 2f - 0.5f;

        float x = Mathf.Round(adjustedX / cellSize) * cellSize + objectSize.x / 2f + 0.5f;
        float y = Mathf.Round(adjustedY / cellSize) * cellSize + objectSize.y / 2f + 0.5f;

        return new Vector2(x, y);
    }
}

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
    private bool inViewport = false;
    // [HideInInspector] public bool isSnapped = false;

    // position when start to drag
    private Vector2 originalPosition;
    // position after drag
    private Vector2 newPosition;

    void Update()
    {
        Drag();
    }

    private void Drag()
    {
        if (isDragging)
        {
            newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector2(newPosition.x, newPosition.y);
        }
    }

    private void OnMouseDown()
    {
        print("on mouse down");
        // isSnapped = false;
        isDragging = true;

        if (tag.Equals("PlayableUI")) GameManager.Instance.OnDragging();

        GetComponent<SpriteRenderer>().sortingOrder = 999;

        originalPosition = transform.position;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (tag.Equals("PlayableUI")) GameManager.Instance.OnRelease();

        GetComponent<SpriteRenderer>().sortingOrder = 1;
        // check if outside of the viewport
        if (inViewport)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            if (canDrop) transform.position = Snap(newPosition, cellSize);
            else
            {
                transform.position = originalPosition;
                inViewport = true;
                canDrop = true;
            }
        }
    }

    private Vector2 Snap(Vector2 position, float cellSize)
    {
        // isSnapped = true;
        float adjustedX = Mathf.Round(position.x - objectSize.x / 2f);
        float adjustedY = Mathf.Round(position.y - objectSize.y / 2f);

        float x = Mathf.Round(adjustedX / cellSize) * cellSize + objectSize.x / 2f;
        float y = Mathf.Round(adjustedY / cellSize) * cellSize + objectSize.y / 2f;

        return new Vector2(x, y);
    }

    // return true if overlapping area > 50% of the grid => Grid glows
    private bool CheckGridState(Collider2D other)
    {
        // Ensure both colliders are BoxCollider2D

        BoxCollider2D otherBox = other as BoxCollider2D;
        Collider2D thisBox = GetComponent<Collider2D>();

        // Calculate overlapping area
        float overlapWidth = Mathf.Min(thisBox.bounds.max.x, otherBox.bounds.max.x) - Mathf.Max(thisBox.bounds.min.x, otherBox.bounds.min.x);
        float overlapHeight = Mathf.Min(thisBox.bounds.max.y, otherBox.bounds.max.y) - Mathf.Max(thisBox.bounds.min.y, otherBox.bounds.min.y);

        if (overlapWidth > 0 && overlapHeight > 0)
        {
            float overlapArea = overlapWidth * overlapHeight;
            float otherBoxArea = otherBox.bounds.size.x * otherBox.bounds.size.y;

            float overlapRatio = overlapArea / otherBoxArea;

            Debug.Log($"Overlap ratio: {overlapRatio * 100}%");

            if (overlapRatio >= 0.5f) return true;
            return false;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = true;
        }

        if (!isDragging) return;
        if (col.tag.Equals("PlayableUI"))
        {
            // print("snap" + name + isSnapped);
            // print("snap" + col.name + isSnapped);
            if (col.GetComponent<DraggableObject>().inViewport) canDrop = false;
            // print(name + canDrop);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = true;
        }

        if (!isDragging) return;
        if (col.tag.Equals("PlayableUI"))
        {
            if (col.GetComponent<DraggableObject>().inViewport) canDrop = false;
        }

        if (col.tag.Equals("Grid"))
        {
            // check if over 50% overlapping
            if (CheckGridState(col))
            {
                col.GetComponent<SingleGrid>().Glow();
            }
            else
            {
                col.GetComponent<SingleGrid>().StopGlow();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = false;
        }

        if (!isDragging) return;
        if (col.tag.Equals("PlayableUI"))
        {
            if (col.GetComponent<DraggableObject>().inViewport) canDrop = true;
            // print(name + canDrop);
            // print("snap" + name + isSnapped);
        }
    }
}

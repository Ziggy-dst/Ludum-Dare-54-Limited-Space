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
    protected bool inViewport = false;
    [HideInInspector]
    public bool canDrop = true;

    // position when start to drag
    protected Vector2 originalPosition;
    // position after drag
    protected Vector2 newPosition;

    public IntersectionCheck intersectionCheck;

    void Update()
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

    protected void CheckStackColliders()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Not In Space", "Heart");
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, layerMask);

            print(hits);

            if (hits.Length == 1) return;

            List<GameObject> objectStack = new List<GameObject>();
            bool hasItem = false;

            foreach (var hit in hits)
            {
                if (hit.collider.tag.Equals("Item")) hasItem = true;
                objectStack.Add(hit.collider.gameObject);
            }

            foreach (var obj in objectStack)
            {
                if (obj.tag.Equals("PlayableUI"))
                {
                    if (hasItem) obj.GetComponent<Collider2D>().isTrigger = false;
                    else obj.GetComponent<Collider2D>().isTrigger = true;
                }
            }
    }

    protected virtual void OnMouseDown()
    {
        isDragging = true;
        CheckStackColliders();
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

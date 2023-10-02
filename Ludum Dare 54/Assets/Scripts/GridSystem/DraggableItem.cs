using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : DraggableObject
{
    private bool inInventory = false;
    private Transform inventory;
    private bool isBlocked = false;
    private bool hasEnteredInventory = true;

    public static GameObject objectBeingDragged;

    private void Start()
    {
        GameManager.Instance.OnReleaseUI += CheckViewportOverlap;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnReleaseUI -= CheckViewportOverlap;
    }

    protected override void Update()
    {
        DragItem();
    }

    private void CheckViewportOverlap()
    {
        if (intersectionCheck.IsPartOutViewport()) inViewport = false;
        else inViewport = true;

        if (!inViewport) Destroy(gameObject);

        // print(inViewport);
    }

    private void DragItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1002;

            int layerMask = 1 << LayerMask.NameToLayer("Heart");

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, layerMask);

            if (hitCollider != null)
            {
                if (hitCollider.gameObject == gameObject)
                {
                    isDragging = true;
                    offset = transform.position - mousePos;
                    objectBeingDragged = gameObject;
                }
            }
            // if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1002;
        }

        if (Input.GetMouseButtonUp(0) && objectBeingDragged == gameObject)
        {
            Cursor.visible = true;

            isDragging = false;
            objectBeingDragged = null;
            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1002;

            CheckViewportOverlap();

            if (inInventory)
            {
                // check if all of the item in the inventory
                // print(intersectionCheck.IsAllInInventory());
                if (intersectionCheck.IsAllInInventory() & !isBlocked) canDrop = true;
                else canDrop = false;

                // if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 2;
                if (canDrop)
                {
                    if (inventory != null) transform.parent = inventory;
                    transform.localPosition = Snap(newPosition);
                }
                else
                {
                    transform.position = originalPosition;
                    inInventory = true;
                    inViewport = true;
                    canDrop = true;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (isDragging)
        {
            newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = true;
        }

        if (col.tag.Equals("InventoryShape"))
        {
            inventory = col.transform.parent;
            inInventory = true;
        }

        if (!isDragging) return;
        if (col.tag.Equals("Item"))
        {
            // if (col.GetComponent<DraggableItem>().inInventory)
            isBlocked = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = true;
        }

        if (col.tag.Equals("InventoryShape"))
        {
            inInventory = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = false;
        }

        if (col.tag.Equals("InventoryShape"))
        {
            inInventory = false;
        }

        if (!isDragging) return;
        if (col.tag.Equals("Item"))
        {
            // if (col.GetComponent<DraggableItem>().inInventory)
            isBlocked = false;
        }
    }

    protected override Vector2 Snap(Vector2 position)
    {
        // print("snap");
        Vector2 localPosition = transform.parent.InverseTransformPoint(position);

        float adjustedX = localPosition.x - objectSize.x / 2f + 0.5f;
        float adjustedY = localPosition.y - objectSize.y / 2f + 0.5f;

        float x = Mathf.Round(adjustedX / cellSize) * cellSize + objectSize.x / 2f - 0.5f;
        float y = Mathf.Round(adjustedY / cellSize) * cellSize + objectSize.y / 2f - 0.5f;

        return new Vector2(x, y);
    }
}

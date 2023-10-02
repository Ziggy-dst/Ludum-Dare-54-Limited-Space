using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : DraggableObject
{
    private bool inInventory = false;
    private Transform inventory;
    // private List<>

    protected override void OnMouseDown()
    {
        isDragging = true;
        CheckStackColliders();
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1002;

        originalPosition = transform.position;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected override void OnMouseUp()
    {
        isDragging = false;
        // if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 1001;

        if (inInventory)
        {
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

    // TODO: add to inventory => add as parent
    // TODO: move out of the scene (not in inventory) => back to the initial position
    // TODO: move out of the inventory => remove item
    // TODO: part (or all) of item out of viewport => remove item

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
            if (col.GetComponent<DraggableItem>().inInventory) canDrop = false;
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

        if (!isDragging) return;
        if (col.tag.Equals("Item"))
        {
            if (col.GetComponent<DraggableItem>().inInventory) canDrop = false;
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
            if (col.GetComponent<DraggableItem>().inInventory) canDrop = true;
        }
    }

    protected override Vector2 Snap(Vector2 position)
    {
        Vector2 localPosition = transform.parent.InverseTransformPoint(position);

        float adjustedX = localPosition.x - objectSize.x / 2f + 0.5f;
        float adjustedY = localPosition.y - objectSize.y / 2f;

        float x = Mathf.Round(adjustedX / cellSize) * cellSize + objectSize.x / 2f - 0.5f;
        float y = Mathf.Round(adjustedY / cellSize) * cellSize + objectSize.y / 2f;

        return new Vector2(x, y);
    }
}

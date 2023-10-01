using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableUI : DraggableObject
{
    private bool inViewport = false;

    protected override void Drag()
    {
        if (isDragging)
        {
            transform.position = new Vector2(newPosition.x, newPosition.y);
        }
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        GameManager.Instance.OnDraggingUI();

        originalPosition = transform.position;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        GameManager.Instance.OnReleaseUI();

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport"))
        {
            inViewport = true;
        }

        if (!isDragging) return;
        if (col.tag.Equals("PlayableUI"))
        {
            if (col.GetComponent<DraggableUI>().inViewport) canDrop = false;
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
            if (col.GetComponent<DraggableUI>().inViewport) canDrop = false;
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
            if (col.GetComponent<DraggableUI>().inViewport) canDrop = true;
        }
    }
}

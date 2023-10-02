using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableUI : DraggableObject
{
    private collideEdge currentCollideEdge = collideEdge.None;

    public float horizontalEdgePos = 22.75f;
    public float verticalEdgePos = 12f;

    private enum collideEdge
    {
        None,
        Right,
        Down,
        Left,
        Up
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        Cursor.visible = false;
        GameManager.Instance.OnDraggingUI();
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        Cursor.visible = true;
        GameManager.Instance.OnReleaseUI();

        if (currentCollideEdge != collideEdge.None) ReturnToEdge();

            // check if outside of the viewport
        if (inViewport)
        {
            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 2;
            if (canDrop) transform.position = Snap(newPosition);
            else
            {
                transform.position = originalPosition;
                inViewport = true;
                canDrop = true;
            }
        }
    }

    private void ReturnToEdge()
    {
        switch (currentCollideEdge)
        {
            case collideEdge.Up:
                transform.position = new Vector2(transform.position.x, verticalEdgePos);
                break;
            case collideEdge.Down:
                transform.position = new Vector2(transform.position.x, -verticalEdgePos);
                break;
            case collideEdge.Right:
                transform.position = new Vector2(horizontalEdgePos, transform.position.y);
                break;
            case collideEdge.Left:
                transform.position = new Vector2(-horizontalEdgePos, transform.position.y);
                break;
        }
    }

    private void DetectEdgeCollider(Collider2D col, bool isSnapping)
    {
        if (!isSnapping)
        {
            currentCollideEdge = collideEdge.None;
            return;
        }
        switch (col.name)
        {
            case "Up":
                currentCollideEdge = collideEdge.Up;
                break;
            case "Down":
                currentCollideEdge = collideEdge.Down;
                break;
            case "Right":
                currentCollideEdge = collideEdge.Right;
                break;
            case "Left":
                currentCollideEdge = collideEdge.Left;
                break;
            default:
                currentCollideEdge = collideEdge.None;
                break;
        }
        // print(currentCollideEdge);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("ScreenBound")) DetectEdgeCollider(col, true);

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
        if (col.tag.Equals("ScreenBound")) DetectEdgeCollider(col, true);

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
        if (col.tag.Equals("ScreenBound")) DetectEdgeCollider(col, false);

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

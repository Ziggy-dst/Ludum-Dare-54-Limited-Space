using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGrid : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private GameObject occupyingUI;

    private GameObject occupiedSprite;
    private GameObject dropSprite;

    public string droppableTag;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        occupiedSprite = transform.GetChild(0).gameObject;
        dropSprite = transform.GetChild(1).gameObject;
    }

    private void Glow(GameObject sprite)
    {
        sprite.SetActive(true);
    }

    private void StopGlow()
    {
        occupiedSprite.SetActive(false);
        dropSprite.SetActive(false);
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
            float thisBoxArea = thisBox.bounds.size.x * thisBox.bounds.size.y;

            float overlapRatio = overlapArea / thisBoxArea;

            // Debug.Log($"Overlap ratio: {overlapRatio * 100}%");

            if (overlapRatio >= 0.15f) return true;
            return false;
        }

        return false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals(droppableTag))
        {
            if (occupyingUI == null) occupyingUI = other.gameObject;
            // if overlap 50%
            if (CheckGridState(other))
            {
                if (!other.gameObject.Equals(occupyingUI)) return;
                // if can drop => green
                if (other.GetComponent<DraggableObject>().canDrop)
                {
                    // if is dragging => green
                    if (other.GetComponent<DraggableObject>().isDragging)
                    {
                        StopGlow();
                        Glow(dropSprite);
                    }
                    // not dragging => red (occupied)
                    else
                    {
                        StopGlow();
                        Glow(occupiedSprite);
                    }
                }
                // otherwise => red
                else
                {
                    StopGlow();
                    Glow(occupiedSprite);
                }
            }
            else
            {
                if (!other.gameObject.Equals(occupyingUI)) return;
                occupyingUI = null;
                StopGlow();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals(droppableTag))
        {
            if (!other.gameObject.Equals(occupyingUI)) return;
            occupyingUI = null;
            StopGlow();
        }
    }
}

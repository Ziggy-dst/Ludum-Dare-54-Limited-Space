using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item:
// not all collide with Viewport => remove,
// not all collide with Inventory grid => return DONE
// no collide with Inventory grid => remove DONE
// UI:
// collide with viewport =>
public class IntersectionCheck : MonoBehaviour
{
    public int maxGrids;
    public DraggableGridCollider[] singleGridColliders;

    private void Start()
    {
        singleGridColliders = GetComponentsInChildren<DraggableGridCollider>();
    }

    public bool IsAllInInventory()
    {
        foreach (var col in singleGridColliders)
        {
            if (!col.isCollidingInventory) return false;
        }

        return true;
    }

    public bool IsPartOutViewport()
    {
        foreach (var col in singleGridColliders)
        {
            if (!col.isCollidingViewport) return true;
        }

        return false;
    }
}

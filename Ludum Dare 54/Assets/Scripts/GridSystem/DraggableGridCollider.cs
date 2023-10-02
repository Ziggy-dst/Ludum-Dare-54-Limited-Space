using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(CircleCollider2D))]
public class DraggableGridCollider : MonoBehaviour
{
    [HideInInspector] public bool isCollidingViewport = false;
    [HideInInspector] public bool isCollidingInventory = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport")) isCollidingViewport = true;
        if (col.tag.Equals("InventoryShape"))
        {
            isCollidingInventory = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Viewport")) isCollidingViewport = false;
        if (col.tag.Equals("InventoryShape")) isCollidingInventory = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item: collide with Viewport, Inventory grid
// UI: collide with viewport

[RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(CircleCollider2D))]
public class DraggableGridCollider : MonoBehaviour
{
    [HideInInspector] public bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

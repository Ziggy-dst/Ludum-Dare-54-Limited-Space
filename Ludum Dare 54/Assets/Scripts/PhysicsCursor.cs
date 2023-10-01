using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCursor : MonoBehaviour
{
    // public LayerMask obstacleLayer; // Set the layer of the obstacle in the inspector

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set to the appropriate value if your game is not on the z=0 plane

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

        if (hit.collider == null) // No obstacle detected
        {
            transform.position = mousePosition;
        }
        else
        {
            print("collide");
        }
        // You can add else statement here to handle the situation when the mouse hits an obstacle
    }
}

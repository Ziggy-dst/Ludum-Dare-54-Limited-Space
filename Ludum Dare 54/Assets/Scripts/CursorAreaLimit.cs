using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAreaLimit : MonoBehaviour
{
    public Vector2 minXMaxY; // Define the min and max values for X and Y coordinates
    private Vector2 currentPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; // Lock the cursor within the window
        Cursor.visible = false; // Hide the system cursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentPos.x += mouseX;
        currentPos.y += mouseY;

        currentPos.x = Mathf.Clamp(currentPos.x, -minXMaxY.x, minXMaxY.x);
        currentPos.y = Mathf.Clamp(currentPos.y, -minXMaxY.y, minXMaxY.y);

        transform.position = currentPos; // Set your game cursor's position
    }
}


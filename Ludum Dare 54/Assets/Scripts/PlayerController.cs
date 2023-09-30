using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    public float moveSpeed;
    public float friction;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        PlayerControl();
        rb2D.velocity *= friction;
    }

    void PlayerControl()
    {
        rb2D.AddForce(moveDirection * moveSpeed);
    }
}

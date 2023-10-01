using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rb2D;
    [HideInInspector] public Vector2 moveDirection;
    public float moveSpeed;
    private float friction;
    public float moveFriction;
    public float brakeFriction;

    public List<Collider2D> nearbyBodies;
    public GameObject nearestBody;
    public float nearestBodyDistance;

    public List<Collider2D> nearbyDangers;
    public GameObject nearestDanger;
    public float nearestDangerDistance;

    void Start()
    {
        instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        friction = moveFriction;
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Brake();
        DetectNearby();
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

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            friction = brakeFriction;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            friction = moveFriction;
        }
    }

    void DetectNearby()
    {
        nearbyBodies.Clear();
        nearbyDangers.Clear();
        
        nearestBodyDistance = Single.PositiveInfinity;
        nearestDangerDistance = Single.PositiveInfinity;
        
        List<Collider2D> potentialNearbyBodies = Physics2D.OverlapCircleAll(transform.position, 100f).ToList();
        foreach (var potentialBodyCollider2D in potentialNearbyBodies)
        {
            if (potentialBodyCollider2D.CompareTag("Heart"))
            {
                nearbyBodies.Add(potentialBodyCollider2D);
            }

            if (potentialBodyCollider2D.CompareTag("Danger"))
            {
                nearbyDangers.Add(potentialBodyCollider2D);
            }
        }

        foreach (var bodyCollider2D in nearbyBodies)
        {
            float distance = (bodyCollider2D.gameObject.transform.position - transform.position).magnitude;
            if (distance < nearestBodyDistance)
            {
                nearestBodyDistance = distance;
                nearestBody = bodyCollider2D.gameObject;
            }
        }

        foreach (var dangerCollider2D in nearbyDangers)
        {
            float distance = (dangerCollider2D.gameObject.transform.position - transform.position).magnitude;
            if (distance < nearestDangerDistance)
            {
                nearestDangerDistance = distance;
                nearestDanger = dangerCollider2D.gameObject;
            }
        }
    }
}

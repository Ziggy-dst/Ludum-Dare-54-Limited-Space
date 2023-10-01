using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceTrash : MonoBehaviour
{
    private GameObject player;

    private Rigidbody2D rb2D;

    public float hitForce;

    public float moveSpeed;

    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = (player.transform.position - transform.position).normalized * moveSpeed;
        rb2D.AddTorque(Random.Range(50f,500f));
        Invoke("SelfDestroy", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>()
                .AddForce((player.transform.position - transform.position).normalized * hitForce, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}

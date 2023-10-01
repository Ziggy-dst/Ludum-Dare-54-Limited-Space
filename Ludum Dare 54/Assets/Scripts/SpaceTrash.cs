using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceTrash : MonoBehaviour
{
    private GameObject player;

    private Rigidbody2D rb2D;

    public float hitForce;

    public float moveSpeed;

    public float destroyTime;

    public Transform trash;

    public Transform trail;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = (player.transform.position - transform.position).normalized * moveSpeed;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rb2D.velocity);
        trash.DORotate(new Vector3(0, 0, 360f), Random.Range(1f, 3f), RotateMode.FastBeyond360).SetEase(Ease.Linear)
            .SetLoops(-1);
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
            trash.DOKill();
            Destroy(gameObject);
        }
    }

    void SelfDestroy()
    {
        trash.DOKill();
        Destroy(gameObject);
    }
}
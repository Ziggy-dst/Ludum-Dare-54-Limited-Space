using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    private bool isPlayingBackwards = false;
    public float hitForce;
    public float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Track"))
        {
            // print("track!");
            if (isPlayingBackwards)
            {
                transform.parent.DOPlayForward();
                // print("forward!");
                isPlayingBackwards = false;
            }
            else if(!isPlayingBackwards)
            {
                transform.parent.DOPlayBackwards();
                // print("back!");
                isPlayingBackwards = true;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject player = col.gameObject;
            player.GetComponent<Rigidbody2D>()
                .AddForce((player.transform.position - transform.position).normalized * hitForce, ForceMode2D.Impulse);
            
            PlayerController.instance.TakeDamage(damage);
        }
    }
}

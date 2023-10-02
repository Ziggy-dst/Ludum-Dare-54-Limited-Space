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

    public float damage;

    // public Transform trail;

    public AudioClip impactClip;
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
            PlayerController.instance.TakeDamage(damage);
            trash.DOKill();
            
            AudioSource audioSource = col.gameObject.AddComponent<AudioSource>();
            audioSource.clip = impactClip;
            audioSource.spatialBlend = 0;
            audioSource.Play();
            StartCoroutine(SelfDestroy(audioSource));

            SelfDestroy();
        }
    }

    // private void OnDestroy()
    // {
    //     StartCoroutine(SelfDestroy(audioSource));
    // }
    
    IEnumerator SelfDestroy(AudioSource audioSource)
    {
        yield return new WaitForSeconds(5f);
        Destroy(audioSource);
    }

    void SelfDestroy()
    {
        trash.DOKill();
        Destroy(gameObject);
    }
}
